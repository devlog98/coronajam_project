using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayer : MonoBehaviour
{
    public GameObject spawnObject;
    GameObject tiledestroy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") || collision.CompareTag("Pausable"))
        {
            tiledestroy = Instantiate(spawnObject, transform.position, transform.rotation);
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Destroy(tiledestroy);
    }
}
