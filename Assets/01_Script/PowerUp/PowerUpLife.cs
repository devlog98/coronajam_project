using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLife : MonoBehaviour
{
    [SerializeField] int life;
    [SerializeField] float timeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("Disable", timeDestroy);
    }

    public void TimePowerUP()
    {
        Invoke("Disable", timeDestroy);
    }

    public void Disable()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            other.gameObject.SendMessage("ReceiveLife", life);
            Disable();
        }
    }
}
