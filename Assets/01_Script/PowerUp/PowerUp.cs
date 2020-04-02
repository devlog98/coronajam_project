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
            amountSpawLife = amountSpawLife - 1;
            GameObject spawPowerUp = Instantiate(spawnPowerUpLife, grids[randomPosition].transform.position, grids[randomPosition].transform.rotation);
            timeSpawn();
    }

    void RoundFinal()
    {
        time = 0;
    }
}
