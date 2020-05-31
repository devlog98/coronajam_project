using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sneeze : MonoBehaviour
{
    [Header("Moviment")]
    public float speed;
    private GameObject[] linha1 = new GameObject[] {null, null, null };
    private GameObject[] linha2 = new GameObject[] { null, null, null };
    private GameObject[] linha3 = new GameObject[] { null, null, null };

    [Header("Sneeze Object")]
    public GameObject spawnObject;
    private GameObject targetGrid;
    private int getRandom;
    bool get = false;
    List<GameObject> listSneeze = new List<GameObject>();
    private bool exist;

    [Header("Sound")]
    [EventRef] public string sneezeSound;

    private void Awake()
    {  
        getTargetGrid();
    }   

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);
    }

    void GetTransform()
    {
        Debug.Log("Entrou no Get Transform");
        for (int i = 1; i <= 10; i++)//Obtem as grids da cena e coloca em 3 vetores
        {
            if (i < 4){linha1[i - 1] = GameObject.Find("Grid " + i);}
            if (i > 3 && i < 7){linha2[i - 4] = GameObject.Find("Grid " + i);}
            if (i > 6 && i < 10){linha3[i - 7] = GameObject.Find("Grid " + i);}
        }
        getTargetGrid();
    }

    public void getTargetGrid()
    {
        if (linha1[0] != null)
        {
            getRandom = Random.Range(0, 3);//Pega a posição do Gameobject e obtem um dos vetores da mesma posição
            if (transform.position.y >= 0.1f)//0.6600001
            {
                targetGrid = linha1[getRandom];
                get = true;
            }
            else if (transform.position.y >= -0.8 || transform.position.y <= -0.9)//-0.9399999
            {
                targetGrid = linha2[getRandom];
                get = true;
            }
            if (transform.position.y == -2.54f)//-2.54
            {
                targetGrid = linha3[getRandom];
                get = true;
            }
        }
        else
        {
            GetTransform();
        }  
    }

    public string grid()
    {
        return targetGrid.name;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {       
        if (collision.gameObject.name == targetGrid.name)//Se a grid de colisão tiver o mesmo nome do gameobject destroi o o objeto e spawna uma grid infectada
        {
            AudioManager.instance.PlayAudioclip(sneezeSound);
            gameObject.SetActive(false);
        }
    }
}
