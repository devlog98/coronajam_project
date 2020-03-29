using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector]public Animator playerAnim;

    //public AudioSource playerAudio;
    //public AudioClip playerDash;

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

    [Header("Sound Effects")]
    [EventRef] public string moveSound;
    [EventRef] public string hitSound;
    private bool canPlayAudio = true;

    void Start() {
        healthUI.StartHealthCounter(health);
        playerAnim = GetComponent<Animator>();
        //playerAudio = GetComponent<AudioSource>();
        canPlayAudio = true;
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Move();

        if (canMove == false)
        {
            click += Time.deltaTime;
            Debug.Log("Entrou " +click);
            if (click >= 0.3f)
            {
                canMove = true;
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

        //Checar inputs do usuário
        if (Input.GetButtonDown("Horizontal") && !horizontal && !vertical)
        {
            moveSide = Input.GetAxis("Horizontal");
            if (moveSide > 0 && right || moveSide < 0 && left) 
            {
                //move para os lados
                if (moveSide > 0)
                {
                    localização = transform.position + sideOffset;
                    
                }
                else
                {
                    localização = transform.position + (-sideOffset);
                    
                }
                canMove = false;
                isIdle = false;
                horizontal = true;               
            }
        }

        if (Input.GetButtonDown("Vertical") && !vertical && !horizontal)
        {
            moveUp = Input.GetAxis("Vertical");
            if (moveUp > 0 && up || moveUp < 0 && down) 
            {
                //move para cima e baixo
                    if (moveUp > 0)
                    {
                        localização = transform.position + upOffset;
                        
                    }
                    else
                    {
                        localização = transform.position + (-upOffset);
                        
                    }
                canMove = false;
                isIdle = false;
                vertical = true;               
            }
        }

        if (Input.GetButtonDown("Cancel")) {
            Pause.instance.TogglePause();
        }
    }

    public void Move()
    {
        //Mover em grid com base nos Physics2dOverlap

        //move para os lados

        if (horizontal) {
            //play movement audio once
            if (canPlayAudio) {
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
        //move para cima e baixo
        if (vertical)
        {
            //play movement audio once
            if (canPlayAudio) {
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }

            //playerAnim.SetTrigger("IsJumping");           
            transform.position = Vector3.MoveTowards(transform.position, localização, speedUp * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                vertical = false;
                isIdle = true;
                canPlayAudio = true;
            }
        }
    }

    void animationPlayer()
    {
        if (moveSide > 0)
        {
            //playerAnim.SetTrigger("IsGoingFront");
            playerAnim.SetBool("IsGoingFront", horizontal);

        }
        else
        {
            //playerAnim.SetTrigger("IsGoingBack");
            playerAnim.SetBool("IsGoingBack", horizontal);

        }

        playerAnim.SetBool("IsIdle", isIdle);
        //playerAnim.SetBool("IsJumping", vertical);
        if (vertical)
        {
            playerAnim.SetTrigger("Jump");
        }
        

    }

    void PhysicsCheck()
    {
        //verificar se pode se locomover, e distancia de cada grid.
        right = Physics2D.OverlapCircle(transform.position + sideOffset, 0.15f, groundCheck);
        left = Physics2D.OverlapCircle(transform.position + (-sideOffset), 0.15f, groundCheck);
        up = Physics2D.OverlapCircle(transform.position + upOffset, 0.15f, groundCheck);
        down = Physics2D.OverlapCircle(transform.position + (-upOffset), 0.15f, groundCheck);
    }

    //responsável por calcular o dano que o jogador sofre
    public void ReceiveDamage(int damage)
    {
        if (!isInvincible) {
            health -= damage;
            healthUI.UpdateHealthCounter(true);
            AudioManager.instance.PlayAudioclip(hitSound);
            StartCoroutine(ActivateInvincibility());
        }
    }

    private IEnumerator ActivateInvincibility()
    {
        isInvincible = true;
        yield return new WaitForSeconds(invincibilityTime);
        isInvincible = false;
    }

    private void OnDrawGizmos()
    {
        //mostra o overlap na unity
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sideOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-sideOffset), 0.15f);
        Gizmos.DrawWireSphere(transform.position + upOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-upOffset), 0.15f);
    }
}