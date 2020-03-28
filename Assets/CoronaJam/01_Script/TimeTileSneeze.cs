using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTileSneeze : MonoBehaviour
{
    [Header("Time")]
    public float timeDestroy;
    public int damage = 1;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, timeDestroy);
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.gameObject.name == "Player") 
        {
            other.gameObject.SendMessage("ReceiveDamage", damage);
        }
    }
}