using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour
{
    [Header("Moviment")]
    public float speed;
    private GameObject[] linha1 = new GameObject[3];
    private GameObject[] linha2 = new GameObject[3];
    private GameObject[] linha3 = new GameObject[3];

    [Header("Sneeze Object")]
    public GameObject spawnObject;
    private GameObject targetGrid;
    private int getRandom;
    bool get = false;
    // Start is called before the first frame update
    void Start()
    {
        GetTransform();
        getTargetGrid();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
        
    }

    void GetTransform()
    {
        for (int i = 1; i <= 10; i++)
        {
            if (i < 4)
            {
                linha1[i - 1] = GameObject.Find("Grid " + i);
            }

            if (i > 3 && i < 7)
            {
                linha2[i - 4] = GameObject.Find("Grid " + i);
            }
            if (i > 6 && i < 10)
            {
                linha3[i - 7] = GameObject.Find("Grid " + i);
            }

        }
    }

    void getTargetGrid()
    {
        getRandom = Random.Range(0,3);
        //-0.25
        //-1.95
        //-3.65
        if (transform.position.y == -0.25f)
        {
            targetGrid = linha1[getRandom];
            Debug.Log(targetGrid.name);
            get = true;
        }
        if (transform.position.y == -1.95f)
        {
            targetGrid = linha2[getRandom];
            Debug.Log(targetGrid.name);
            get = true;
        }
        if (transform.position.y == -3.65f)
        {
            targetGrid = linha3[getRandom];
            Debug.Log(targetGrid.name);
            get = true;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == targetGrid.name)
        {
            GameObject spawVirus = Instantiate(spawnObject, collision.transform.position, collision.transform.rotation);
            Destroy(gameObject);
        }
    }
}
