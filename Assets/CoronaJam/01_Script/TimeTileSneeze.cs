using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeTileSneeze : MonoBehaviour
{
    [Header("Time")]
    private float timeDestroy;

    // Start is called before the first frame update
    void Start()
    {
        timeDestroy = 2.0f;
        Destroy(gameObject, timeDestroy);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
