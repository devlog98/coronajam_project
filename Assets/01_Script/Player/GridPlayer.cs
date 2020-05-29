using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayer : MonoBehaviour
{
    public GameObject spawnObject;
    GameObject tiledestroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") || collision.CompareTag("Pausable"))
        {
            tiledestroy = Instantiate(spawnObject, transform.position, transform.rotation);
        }  
    }

    private void OnTriggerExit2D(Collider2D collision){Destroy(tiledestroy);}
}
