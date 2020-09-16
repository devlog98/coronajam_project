using FMODUnity;
using MSuits.Audio;
using System;
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
    bool animMovement = false;
    bool checkAttack = false;
    bool canMove = true;

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
    List<GameObject> listFire = new List<GameObject>();
    List<GameObject> listSneeze = new List<GameObject>();

    public Animator VisitorAnim;

    [Header("Sound Effects")]
    [EventRef] public string moveSound;
    [EventRef] public string virusShotSound;
    [EventRef] public string virusSneezeSound;
    [EventRef] public string deathSound;

    private bool canPlayAudio = true;

    void Update()
    {
        if (canMove) {
            timeControl();
            control();
            CheckInput();
            Move();
            VisitorAnim.SetBool("IsMoving", animMovement);
            PhysicsCheck();
        }
    }

    void timeControl()
    {       
        timeM += Time.deltaTime;// a cada um segundo as variaveis time recebem um incremento.
        if (virusSneeze){timeV += Time.deltaTime;}
        if (virusShot){timeS += Time.deltaTime;}
    }

    public void control()
    { 
        if (timeM >= timeMovement && !checkAttack)//quando o time tiver um valor igual ou maior ao especificado para o movimento é realizado a ação
        {
            timeM = 0;
            getRandom = UnityEngine.Random.Range(0, sense.Length);//Obtem uma direção e sentido aleatório dos arrays.
            getDirection = direction[getRandom];
            getRandom = UnityEngine.Random.Range(0, sense.Length);
            getSense = sense[getRandom];
            if (getDirection == "Horizontal" && getSense > 0 && !right || getDirection == "Horizontal" && getSense < 0 && !left || getDirection == "Vertical" && getSense > 0 && !up || getDirection == "Vertical" && getSense < 0 && !down) { timeM = 3; }
        }

        if (!animMovement)
        {
            if (timeV >= timeSneeze && virusSneeze)//quando o time tiver um valor igual ou maior ao especificado para o ataque é realizado a ação
            {
                checkAttack = true;
                //Debug.Log("Sneeze " + timeV);
                timeV = 0;
                timeS = 0;
                VisitorAnim.SetTrigger("IsSneezing");
                AudioManager.instance.PlayAudioclip(virusSneezeSound);
                Invoke("Sneeze", 0.4f);
            }
            else if (timeS >= timeVirusShot && virusShot)
            {
                checkAttack = true;
                //Debug.Log("Viorus Shot " + timeS);
                timeS = 0;
                VisitorAnim.SetTrigger("IsCoughing");
                AudioManager.instance.PlayAudioclip(virusShotSound);
                Invoke("Fire", 0.5f);
            }
        }
    }

    public void CheckInput()
    {   
        if (getDirection == "Horizontal" && !horizontal && !vertical)//Checar se a direção e sentidos estão disponiveis, se disponivel a variavel da direção recebe verdadeiro
        {
            getDirection = null;
            if (getSense > 0 && right || getSense < 0 && left) { walkSide = true; animMovement = true;}
        }
        if (getDirection == "Vertical" && !vertical && !horizontal)
        {
         getDirection = null;
         if (getSense > 0 && up || getSense < 0 && down) { walkUp = true; animMovement = true;}
        }
    }

    public void Move()
    {
        if (walkSide)//Mover em grid com base nos Physics2dOverlap, obtem o sentido da direção Horizontal
        {
            walkSide = false;
            if (getSense > 0){localização = transform.position + sideOffset;}
            else
            {localização = transform.position + (-sideOffset);}
            horizontal = true;
        }
        if (walkUp)//obtem o sentido da direção Vertical
        {
            walkUp = false;
            if (getSense > 0){localização = transform.position + upOffset;}
            else{localização = transform.position + (-upOffset);}
            vertical = true;
        }       
        if (horizontal)//move para os lados
        {
            VisitorAnim.SetTrigger("IsMoving");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);           
            if (canPlayAudio)//play movement audio once
            {
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }
            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                animMovement = false; 
                horizontal = false;
                canPlayAudio = true;
            }
        }
        if (vertical)//move para cima e baixo
        {  
            if (canPlayAudio){//play movement audio once
                AudioManager.instance.PlayAudioclip(moveSound);
                canPlayAudio = false;
            }
            VisitorAnim.SetTrigger("IsMoving");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedUp * Time.deltaTime);
            if (Vector3.Distance(transform.position, localização) == 0f){
                animMovement = false;
                vertical = false;
                canPlayAudio = true;
            }
        }
    }

    void PhysicsCheck()
    {
        right = Physics2D.OverlapCircle(transform.position + sideOffset, 0.15f, groundCheck);//verificar se pode se locomover, e distancia de cada grid.
        left = Physics2D.OverlapCircle(transform.position + (-sideOffset), 0.15f, groundCheck);
        up = Physics2D.OverlapCircle(transform.position + upOffset, 0.15f, groundCheck);
        down = Physics2D.OverlapCircle(transform.position + downOffset, 0.15f, groundCheck);
    }

    void Fire()
    {
        if (listFire.Count < 5){ listFire.Add(Instantiate(virusObject, distanceAttack + transform.position, transform.rotation)); }
        else
        {
            listFire[0].gameObject.SetActive(true);
            listFire.Add(listFire[0].gameObject);
            listFire[0].gameObject.transform.position = distanceAttack + transform.position;
            listFire.RemoveAt(0);
        }       
        checkAttack = false;
    }

    void Sneeze()
    {
        if(listSneeze.Count < 5){ listSneeze.Add(Instantiate(SneezeObject, distanceAttack + transform.position, transform.rotation));}
        else
        {          
            listSneeze.Add(listSneeze[0].gameObject);            
            listSneeze[0].gameObject.transform.position = distanceAttack + transform.position;
            listSneeze[0].gameObject.SetActive(true);
            listSneeze[0].gameObject.GetComponent<Sneeze>().getTargetGrid();          
            listSneeze.RemoveAt(0);
        }
        checkAttack = false;
    }

    public void SetDifficulty(EnemyDifficulty difficulty)//altera a dificuldade do inimigo baseado no round
    {
        speedSide = difficulty.SpeedSide;//movimento do inimigo
        speedUp = difficulty.SpeedUp;
        timeMovement = difficulty.TimeMovement;//ataques do inimigo
        timeVirusShot = difficulty.TimeVirusShot;
        virusShot = difficulty.VirusShot;
        timeSneeze = difficulty.TimeSneeze;
        virusSneeze = difficulty.VirusSneeze;
    }

    public void LockMove() {canMove = false; }//locks enemy movement
 
    public void Die(Action<bool> callback) {//plays death animation
        LockMove();
        VisitorAnim.SetTrigger("Death"); //trigger death
        AudioManager.instance.PlayAudioclip(deathSound); //trigger sound
        StartCoroutine(WaitForDeathAnimation(callback)); //trigger wait coroutine
    }

    private IEnumerator WaitForDeathAnimation(Action<bool> callback) { //return to GM when death animation is over
        yield return null; //wait death anim to compute
        yield return new WaitForSeconds(VisitorAnim.GetCurrentAnimatorStateInfo(0).length); //wait duration of death animation
        callback(true); //activate callback
    }

    /*private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;//mostra o overlap na unity
        Gizmos.DrawWireSphere(transform.position + sideOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-sideOffset), 0.15f);
        Gizmos.DrawWireSphere(transform.position + upOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + downOffset, 0.15f);
    }*/
  
    public void SwapDeathSound(string sound){deathSound = sound; }//swaps death sound of the enemy
}