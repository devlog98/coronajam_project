using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]public Animator playerAnim;

    [Header("Movement")]
    private float moveSide;
    private float moveUp;
    public float speedSide;
    public float speedUp;
    public bool left, right, up, down;
    public bool isIdle;

    [Header("Check")]
    public LayerMask groundCheck;
    public Vector3 sideOffset, upOffset;
    bool horizontal;
    bool vertical;
    bool jump = false;
    Vector3 localização;
    bool walkSide;
    bool walkUp;
    float click = 0.5f;
    bool canMove = true;

    [Header("Health")]
    public int health = 3;
    public PlayerUI healthUI;
    public float invincibilityTime = 2f;
    private bool isInvincible;

    [Header("IFrame Stuff")]
    public Color flashColor;
    public Color regularColor;
    public float flashDuration;
    public int numberofFlashes;
    public SpriteRenderer playerSprite;

    [Header("Sound Effects")]
    [EventRef] public string moveSound;
    [EventRef] public string hitSound;
    [EventRef] public string powerupSound;
    [EventRef] public string deathSound;
    private bool canPlayAudio = true;

    private void Start() {
        healthUI.StartHealthCounter(health);
        playerAnim = GetComponent<Animator>();
        canPlayAudio = true;
    }

    private void Update()
    {
        CheckInput();
        Move();

        if (canMove == false)
        {
            click += Time.deltaTime;
            if (click >= 0.3f && health > 0)
            {
                UnlockMove();
                click = 0;
            }
        }
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
        animationPlayer();
    }

    public void CheckInput()
    {
        if (!canMove)
            return;
        
        InputManager.GetInput();//Checar inputs do usuário

        if (InputManager.GetHorizontalDown() && !horizontal && !vertical)
        {
            moveSide = InputManager.GetHorizontal();
            if (moveSide > 0 && right || moveSide < 0 && left) 
            {               
                if (moveSide > 0)//move para os lados
                {
                    localização = transform.position + sideOffset;                 
                }
                else
                {
                    localização = transform.position + (-sideOffset);     
                }
                LockMove();
                isIdle = false;
                horizontal = true;               
            }
        }

        if (InputManager.GetVerticalDown() && !vertical && !horizontal)
        {
            moveUp = InputManager.GetVertical();
            if (moveUp > 0 && up || moveUp < 0 && down) 
            {
                if (moveUp > 0)//move para cima e baixo
                {
                    localização = transform.position + upOffset;
                        
                }
                else
                {
                    localização = transform.position + (-upOffset);
                        
                }
                LockMove();
                isIdle = false;
                jump = true;
                vertical = true;               
            }
        }

        if (Input.GetButtonDown("Cancel")) {Pause.instance.TogglePause();}
    }

    public void Move()
    {
        
        if (horizontal) {//Mover em grid com base nos Physics2dOverlap, move para os lados
                if (canPlayAudio) {//play movement audio once
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }

            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                horizontal = false;
                isIdle = true;
                canPlayAudio = true;
            }
        }
       
        if (vertical)//move para cima e baixo
        {
            if (canPlayAudio) {//play movement audio once
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }
                      
            transform.position = Vector3.MoveTowards(transform.position, localização, speedUp * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                vertical = false;
                jump = false;
                isIdle = true;
                canPlayAudio = true;
            }
        }
    }

    void animationPlayer()
    {
        if (moveSide > 0)
        {
            playerAnim.SetBool("IsGoingFront", horizontal);
        }
        else
        {         
            playerAnim.SetBool("IsGoingBack", horizontal);
        }
        playerAnim.SetBool("IsIdle", isIdle);
        playerAnim.SetBool("IsJumping", jump);       
    }

    void PhysicsCheck()
    {
        right = Physics2D.OverlapCircle(transform.position + sideOffset, 0.15f, groundCheck);//verificar se pode se locomover, e distancia de cada grid.
        left = Physics2D.OverlapCircle(transform.position + (-sideOffset), 0.15f, groundCheck);
        up = Physics2D.OverlapCircle(transform.position + upOffset, 0.15f, groundCheck);
        down = Physics2D.OverlapCircle(transform.position + (-upOffset), 0.15f, groundCheck);
    }
    
    public void ReceiveDamageFromAttack(int damage) { //method to be used for attack damages
        ReceiveDamage(damage, false);
    }
   
    public void ReceiveDamageFromDialogue(int damage) { //method to be used for dialogue damages
        ReceiveDamage(damage, true);
    }
  
    private void ReceiveDamage(int damage, bool fromDialogue) //responsável por calcular o dano que o jogador sofre
    {
        if (health != 0 && (fromDialogue || !isInvincible)) {
            Vibrator.Vibrate();//comando para vibrar ao levar dano
            health -= damage;
            healthUI.UpdateHealthCounter(true);
            AudioManager.instance.PlayAudioclip(hitSound);

            if (health > 0) { //trigger hit               
                playerAnim.SetTrigger("IsGettingDamage");
                StartCoroutine(ActivateInvincibility());
                StartCoroutine(FlashCo());
            }
            else {GM.instance.LevelFailed(fromDialogue);}//trigger game over
        }
    }

    public void ReceiveLife(int life)
    {
        AudioManager.instance.PlayAudioclip(powerupSound);
        if (health < 3)
        {
            health += life;
            healthUI.UpdateHealthCounter(false);
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        while (temp < numberofFlashes)
        {
            playerSprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            playerSprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
    }

    
    public void LockMove() { canMove = false; }//locks and unlocks enemy movement

    public void UnlockMove() { canMove = true; }
  
    public void Die(Action<bool> callback) {//plays death animation 
        LockMove();
        playerAnim.SetTrigger("Death"); //trigger death
        AudioManager.instance.PlayAudioclip(deathSound); //trigger sound
        StartCoroutine(WaitForDeathAnimation(callback)); //trigger wait coroutine
    }
   
    private IEnumerator WaitForDeathAnimation(Action<bool> callback) {//return to GM when death animation is over
        yield return null; //wait death anim to compute
        yield return new WaitForSeconds(playerAnim.GetCurrentAnimatorStateInfo(0).length); //wait duration of death animation
        callback(true); //activate callback
    }

    /*private void OnDrawGizmos()
    {
        //mostra o overlap na unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sideOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-sideOffset), 0.15f);
        Gizmos.DrawWireSphere(transform.position + upOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-upOffset), 0.15f);
    }*/
}