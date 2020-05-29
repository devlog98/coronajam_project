using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShot : MonoBehaviour
{
    [Header("Bullet")]
    public float speed;
    public int damage = 1;
    private float timeDestroy;

    void Start()
    {
        timeDestroy = 5.0f;
        Destroy(gameObject, timeDestroy);
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.SendMessage("ReceiveDamageFromAttack", damage);
            Destroy(gameObject);
        }
    }
}