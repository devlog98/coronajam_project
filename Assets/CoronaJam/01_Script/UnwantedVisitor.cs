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
    public Vector3 sideOffset, upOffset;
    bool leftSide, rightSide, upSide, downSide;
    bool horizontal;
    bool vertical;
    Vector3 localização;
    bool walkSide;
    bool walkUp;


    [Header("Control")]
    public float timeMovement;
    public float timeAttack;
    string[] direction = new[] { "Horizontal", "Vertical" };
    int[] sense = new[] {-1, 1 };
    string getDirection;
    int getSense;
    int getRandom;
    float timeM;
    float timeA;

    [Header("Attack")]
    public bool virusShot;
    public GameObject virusObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        control();
        CheckInput();
        Move();
    }

    private void FixedUpdate()
    {

        PhysicsCheck();
    }

    public void control()
    {
        //a cada um segundo as variaveis time recebem um incremento.
        timeM += Time.deltaTime;
        timeA += Time.deltaTime;

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

        //quando o time tiver um valor igual ou maior ao especificado para o ataque é realizado a ação
        if (timeA >= timeAttack)
        {
            timeA = 0;

            if (virusShot)
            {
                Fire();
            }
            
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
            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);

            if (Vector3.Distance(transform.position, localização) == 0f)
            {
                horizontal = false;
            }


        }
        //move para cima e baixo
        if (vertical)
        {
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
        rightSide = Physics2D.OverlapCircle(transform.position + sideOffset, 0.15f, groundCheck);
        leftSide = Physics2D.OverlapCircle(transform.position + (-sideOffset), 0.15f, groundCheck);
        upSide = Physics2D.OverlapCircle(transform.position + upOffset, 0.15f, groundCheck);
        downSide = Physics2D.OverlapCircle(transform.position + (-upOffset), 0.15f, groundCheck);


        if (rightSide)
        {
            right = true;
        }
        else
        {
            right = false;
        }
        if (leftSide)
        {
            left = true;
        }
        else
        {
            left = false;
        }
        if (upSide)
        {
            up = true;
        }
        else
        {
            up = false;
        }
        if (downSide)
        {
            down = true;
        }
        else
        {
            down = false;
        }
    }

    void Fire()
    {
        GameObject cloneVirus = Instantiate(virusObject, transform.position, transform.rotation);
    }

    private void OnDrawGizmos()
    {
        //mostra o overlap na unity
        Gizmos.color = Color.blue;

        Gizmos.DrawWireSphere(transform.position + sideOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-sideOffset), 0.15f);
        Gizmos.DrawWireSphere(transform.position + upOffset, 0.15f);
        Gizmos.DrawWireSphere(transform.position + (-upOffset), 0.15f);
    }
}
