using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirusShot : MonoBehaviour
{
    [Header("Bullet")]
    public float speed;
    public int damage = 1;
    private float timeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        timeDestroy = 8.0f;
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
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