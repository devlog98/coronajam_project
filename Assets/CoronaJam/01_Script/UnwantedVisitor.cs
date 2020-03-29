using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnwantedVisitor : MonoBehaviour
{
    [Header("Movement")]
    public float speedSide;
    public float speedUp;
    public bool left, right, up, down;

    [Header("Check")]
    public LayerMask groundCheck;
    public Vector3 sideOffset, upOffset, downOffset;
    bool horizontal;
    bool vertical;
    Vector3 localização;
    bool walkSide;
    bool walkUp;


    [Header("Control")]
    public float timeMovement;
    string[] direction = new[] { "Horizontal", "Vertical" };
    int[] sense = new[] { -1, 1 };
    string getDirection;
    int getSense;
    int getRandom;
    float timeM;


    [Header("Attack")]
    Vector3 distanceAttack = new Vector3(-1.5f, 0f, 0f);
    public bool virusShot;
    public GameObject virusObject;
    public float timeVirusShot;
    float timeV;
    public bool virusSneeze;
    public GameObject SneezeObject;
    public float timeSneeze;
    float timeS;

    public Animator VisitorAnim;

    [Header("Sound Effects")]
    [EventRef] public string moveSound;
    [EventRef] public string virusShotSound;
    [EventRef] public string virusSneezeSound;
    private bool canPlayAudio = true;

    // Update is called once per frame
    void Update()
    {
        timeControl();
        control();
        CheckInput();
        Move();
    }

    private void FixedUpdate()
    {
        PhysicsCheck();
    }

    void timeControl()
    {
        // a cada um segundo as variaveis time recebem um incremento.
        timeM += Time.deltaTime;
        if (virusSneeze)
        {
            //Debug.Log("time " + timeV);
            timeV += Time.deltaTime;
        }
        if (virusShot)
        {
            //Debug.Log("time " + timeS);
            timeS += Time.deltaTime;
        }
    }

    public void control()
    {
        //quando o time tiver um valor igual ou maior ao especificado para o movimento é realizado a ação
        if (timeM >= timeMovement)
        {
            timeM = 0;
            //Obtem uma direção e sentido aleatório dos arrays.
            getRandom = Random.Range(0, sense.Length);
            getDirection = direction[getRandom];
            getRandom = Random.Range(0, sense.Length);
            getSense = sense[getRandom];
            if (getDirection == "Horizontal" && getSense > 0 && !right || getDirection == "Horizontal" && getSense < 0 && !left || getDirection == "Vertical" && getSense > 0 && !up || getDirection == "Vertical" && getSense < 0 && !down) { timeM = 3; }
        }
        else if (timeV >= timeSneeze && virusSneeze && !horizontal || timeV >= timeSneeze && virusSneeze && !vertical)//quando o time tiver um valor igual ou maior ao especificado para o ataque é realizado a ação
        {
            Debug.Log("Sneeze " + timeV);
            timeV = 0;
            timeS = 0;
            Sneeze();
        }
        else if (timeS >= timeVirusShot && virusShot && !horizontal || timeS >= timeVirusShot && virusShot && !vertical)
        {
            Debug.Log("Viorus Shot " + timeS);
            timeS = 0;
            Fire();
        }
        else if (horizontal && timeS >= timeVirusShot && virusShot || vertical && timeS >= timeVirusShot && virusShot)
        {
            timeS = 2 - (Mathf.RoundToInt(timeS));
            Debug.Log("Vertical " + timeS);
        }
        else if (horizontal && timeV >= timeSneeze && virusSneeze || vertical && timeV >= timeSneeze && virusSneeze)
        {
            timeV = 2 - (Mathf.RoundToInt(timeV));
            Debug.Log("Forizontal " + timeV);
        }
    }

    public void CheckInput()
    {
        //Checar se a direção e sentidos estão disponiveis, se disponivel a variavel da direção recebe verdadeiro
        if (getDirection == "Horizontal" && !horizontal && !vertical)
        {
            getDirection = null;
            if (getSense > 0 && right || getSense < 0 && left) { walkSide = true; }
        }
        if (getDirection == "Vertical" && !vertical && !horizontal)
        {
            getDirection = null;
            if (getSense > 0 && up || getSense < 0 && down) { walkUp = true; }
        }
    }

    public void Move()
    {
        
        //Mover em grid com base nos Physics2dOverlap
        //obtem o sentido da direção Horizontal
        if (walkSide)
        {
            walkSide = false;
            if (getSense > 0)
            {
                localização = transform.position + sideOffset;
            }
            else
            {
                localização = transform.position + (-sideOffset);
            }
            horizontal = true;
        }
        //obtem o sentido da direção Vertical
        if (walkUp)
        {
            walkUp = false;
            if (getSense > 0)
            {
                localização = transform.position + upOffset;
            }
            else
            {
                localização = transform.position + (-upOffset);
            }
            vertical = true;
        }

        //move para os lados
        if (horizontal)
        {
            //play movement audio once
            if (canPlayAudio) {
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }

            VisitorAnim.SetTrigger("IsMoving");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                horizontal = false;
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

            VisitorAnim.SetTrigger("IsMoving");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedUp * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                vertical = false;
                canPlayAudio = true;
            }
        }
    }

    void PhysicsCheck()
    {
        //verificar se pode se locomover, e distancia de cada grid.
        right = Physics2D.OverlapCircle(transform.position + sideOffset, 0.15f, groundCheck);
        left = Physics2D.OverlapCircle(transform.position + (-sideOffset), 0.15f, groundCheck);
        up = Physics2D.OverlapCircle(transform.position + upOffset, 0.15f, groundCheck);
        down = Physics2D.OverlapCircle(transform.position + downOffset, 0.15f, groundCheck);
    }

    void Fire()
    {
        GameObject cloneVirus = Instantiate(virusObject, distanceAttack + transform.position, transform.rotation);
        VisitorAnim.SetTrigger("IsCoughing");
        AudioManager.instance.PlayAudioclip(virusShotSound);
    }

    void Sneeze()
    {
        GameObject cloneSneeze = Instantiate(SneezeObject, distanceAttack + transform.position, transform.rotation);
        VisitorAnim.SetTrigger("IsSneezing");
        AudioManager.instance.PlayAudioclip(virusSneezeSound);
    }

    //altera a dificuldade do inimigo baseado no round
    public void SetDifficulty(EnemyDifficulty difficulty) {
        //movimento do inimigo
        speedSide = difficulty.SpeedSide;
        speedUp = difficulty.SpeedUp;

        //ataques do inimigo
        timeMovement = difficulty.TimeMovement;
        timeVirusShot = difficulty.TimeVirusShot;
        virusShot = difficulty.VirusShot;
        timeSneeze = difficulty.TimeSneeze;
        virusSneeze = difficulty.VirusSneeze;
    }

    private void OnDrawGizmos()
    {
        //mostra o overlap na unity
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position + sideOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-sideOffset), 0.15f);
        Gizmos.DrawWireSphere(transform.position + upOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + downOffset, 0.15f);
    }
}
