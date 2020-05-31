using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPlayer : MonoBehaviour
{
    public GameObject spawnObject;
    public GameObject spawnObject2;
    private bool exist;
    int qtdObjetos = 0;
    private int number;
    List<GameObject> tile = new List<GameObject>();
    List<GameObject> tileSneeze = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack") || collision.CompareTag("Pausable") || collision.CompareTag("Attack2") || collision.CompareTag("PowerUp"))//Qualquer Objeto que passar pela chão spawma um chão diferente
        {           
            if (tile.Count > 0)
            {
                for (int i = 0; i <= tile.Count; i++)
                {
                    if (tile[i].gameObject.transform.position == transform.position)
                    {
                        exist = true;
                        number = i;
                        break;
                    }
                    else
                    {
                        exist = false;
                    }
                }

                if (exist)
                {
                    Cont();
                    tile[number].gameObject.SetActive(true);                   
                }
                else
                {
                    Cont();
                    tile.Add(Instantiate(spawnObject, transform.position, transform.rotation));
                }
            }
            else
            {
                Cont();
                tile.Add(Instantiate(spawnObject, transform.position, transform.rotation));
            }
        }

        if (collision.CompareTag("Attack2"))//quando o catarro entra em contato com a tile spawma o chão contaminado
        {
            string name = collision.gameObject.GetComponent<Sneeze>().grid();
            if (tileSneeze.Count > 0)
            {
                for (int i = 0; i <= tileSneeze.Count; i++)
                {
                    if (tileSneeze[i].gameObject.transform.position == transform.position && gameObject.name == name)
                    {
                        exist = true;
                        number = i;
                        break;
                    }
                    else
                    {
                        exist = false;
                    }
                }

                if (exist)
                {
                    if (tileSneeze[number].gameObject.activeInHierarchy == true)
                    {
                        tileSneeze[number].gameObject.GetComponent<TimeTileSneeze>().CancelDisable();
                    }
                    else
                    {
                        tileSneeze[number].gameObject.SetActive(true);
                        tileSneeze[number].gameObject.GetComponent<TimeTileSneeze>().Disable();
                    }
                    
                }
                else
                {
                    if (gameObject.name == name)
                    {
                        tileSneeze.Add(Instantiate(spawnObject2, transform.position, transform.rotation));
                    }                    
                }
            }
            else
            {
                if (gameObject.name == name)
                {
                    tileSneeze.Add(Instantiate(spawnObject2, transform.position, transform.rotation));
                }
            }

        }
    }

    void Cont()
    {
        qtdObjetos = qtdObjetos + 1;     
    }

    private void OnTriggerExit2D(Collider2D collision){

        foreach (GameObject tilelist in tile)
        {
            if (tilelist.gameObject.transform.position == transform.position)
            {               
                qtdObjetos = qtdObjetos - 1;
                if (qtdObjetos <= 0)
                {
                    tilelist.gameObject.SetActive(false);  
                }
                break;
            }            
        }
    }
}
