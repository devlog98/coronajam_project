using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTileSneeze : MonoBehaviour
{
    [Header("Time")]
    public float timeDisable;
    public int damage = 1;

    void Start(){ Disable(); }

    void SneezeDisable()
    {
        gameObject.SetActive(false);
    }

    public void Disable()
    {
        Invoke("SneezeDisable", timeDisable);       
    }

    public void CancelDisable()
    {
        CancelInvoke("SneezeDisable");
        Invoke("SneezeDisable", timeDisable);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.name == "Player"){other.gameObject.SendMessage("ReceiveDamageFromAttack", damage);}
    }
}