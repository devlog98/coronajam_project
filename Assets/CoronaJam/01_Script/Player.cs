using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool canPlayAudio = true;

    [HideInInspector]public Animator playerAnim;

    //public AudioSource playerAudio;
    //public AudioClip playerDash;

    [Header("Movement")]
    private float moveSide;
    private float moveUp;
    public float speedSide;
    public float speedUp;
    public bool left, right, up, down;

    [Header("Check")]
    public LayerMask groundCheck;
    public Vector3 sideOffset, upOffset;
    bool horizontal;
    bool vertical;
    Vector3 localização;
    bool walkSide;
    bool walkUp;

    [Header("Health")]
    public int health = 3;
    public PlayerUI healthUI;
    public float invincibilityTime = 2f;  
    private bool isInvincible;

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
    }
    private void FixedUpdate()
    {
        PhysicsCheck();
    }

    public void CheckInput()
    {
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
                    vertical = true;
                
            }
        }
    }

    public void Move()
    {
        StartCoroutine(PlayDashAudio());
        //Mover em grid com base nos Physics2dOverlap



        //move para os lados
        if (horizontal)
        {
            if(moveSide > 0)
            {
                playerAnim.SetTrigger("IsGoingFront");
                
            }
            else 
            {
                playerAnim.SetTrigger("IsGoingBack");
                
            }

            
            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                horizontal = false;
            }
        }
        //move para cima e baixo
        if (vertical)
        {
            playerAnim.SetTrigger("IsJumping");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedUp * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                vertical = false;
            }
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

    public IEnumerator PlayDashAudio()
    {
        if (canPlayAudio)
        {
            //playerAudio.PlayOneShot(playerDash);
            canPlayAudio = false;
        }
        
        yield return new WaitForSeconds(speedSide);
        canPlayAudio = true;

    }
}
