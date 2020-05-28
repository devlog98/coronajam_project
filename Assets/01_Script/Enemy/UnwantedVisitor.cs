﻿using FMODUnity;
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

    public Animator VisitorAnim;

    [Header("Sound Effects")]
    [EventRef] public string moveSound;
    [EventRef] public string virusShotSound;
    [EventRef] public string virusSneezeSound;
    [EventRef] public string deathSound;

    private bool canPlayAudio = true;

    // Update is called once per frame
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
        if (timeM >= timeMovement && !checkAttack)
        {
            timeM = 0;
            //Obtem uma direção e sentido aleatório dos arrays.
            getRandom = UnityEngine.Random.Range(0, sense.Length);
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
        //else
        //{
        //    if (timeS >= timeVirusShot && virusShot)
        //    {
        //        timeS = 0.5f - (Mathf.RoundToInt(timeS));
        //        Debug.Log("Vertical " + timeS);
        //    }
        //    else if (timeV >= timeSneeze && virusSneeze)
        //    {
        //        timeV = 0.5f - (Mathf.RoundToInt(timeV));
        //        Debug.Log("Forizontal " + timeV);
        //    }
        //}
    }

    public void CheckInput()
    {
            //Checar se a direção e sentidos estão disponiveis, se disponivel a variavel da direção recebe verdadeiro
            if (getDirection == "Horizontal" && !horizontal && !vertical)
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
            VisitorAnim.SetTrigger("IsMoving");
            transform.position = Vector3.MoveTowards(transform.position, localização, speedSide * Time.deltaTime);
            //play movement audio once
            if (canPlayAudio)
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
                animMovement = false;
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
        checkAttack = false;
    }

    void Sneeze()
    {                
        GameObject cloneSneeze = Instantiate(SneezeObject, distanceAttack + transform.position, transform.rotation);
        checkAttack = false;
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

    //locks enemy movement
    public void LockMove() {
        canMove = false;
    }

    //plays death animation 
    public void Die(Action<bool> callback) {
        LockMove();
        VisitorAnim.SetTrigger("Death"); //trigger death
        AudioManager.instance.PlayAudioclip(deathSound); //trigger sound
        StartCoroutine(WaitForDeathAnimation(callback)); //trigger wait coroutine
    }

    //return to GM when death animation is over
    private IEnumerator WaitForDeathAnimation(Action<bool> callback) {
        yield return null; //wait death anim to compute
        yield return new WaitForSeconds(VisitorAnim.GetCurrentAnimatorStateInfo(0).length); //wait duration of death animation
        callback(true); //activate callback
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

    //swaps death sound of the enemy
    public void SwapDeathSound(string sound) {
        deathSound = sound;
    }
}