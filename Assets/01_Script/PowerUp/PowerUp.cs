using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [Header("Spawn Point")]
    [SerializeField] GameObject[] grids = new GameObject[9];

    [Header("PowerUps")]
    [SerializeField] bool powerUpLife;
    [SerializeField] GameObject spawnPowerUpLife;
    [SerializeField] int amountSpawLife;
    [SerializeField] int timeIntervalminLife;
    [SerializeField] int timeIntervalmaxLife;
    List<GameObject> powerUp = new List<GameObject>();

    private float time;
    private int getRandom1;

    // Start is called before the first frame update
    void Start()
    {
        timeSpawn();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if (powerUpLife && amountSpawLife > 0)
        {
            if (time >= getRandom1)
            {
                RoundFinal();
                spawnPowerupLife();
            }
        }
    }

    void timeSpawn()
    {
        getRandom1 = Random.Range(timeIntervalminLife, timeIntervalmaxLife);
    }

    void spawnPowerupLife()
    {
        int randomPosition = Random.Range(0, 8);
        if (powerUp.Count > 1)
        {
            powerUp.Add(powerUp[0].gameObject);
            powerUp[0].gameObject.transform.position = grids[randomPosition].transform.position;
            powerUp[0].gameObject.transform.rotation = grids[randomPosition].transform.rotation;
            powerUp[0].gameObject.SetActive(true);
            powerUp[0].gameObject.GetComponent<PowerUpLife>().TimePowerUP();
            powerUp.RemoveAt(0);
            ResetStatus();
        }
        else
        {
            powerUp.Add(Instantiate(spawnPowerUpLife, grids[randomPosition].transform.position, grids[randomPosition].transform.rotation));
            ResetStatus();
        }          
    }

    void ResetStatus()
    {
        amountSpawLife = amountSpawLife - 1;
        timeSpawn();
    }

    void RoundFinal()
    {
        time = 0;
    }
}
