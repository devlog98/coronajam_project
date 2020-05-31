using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneBoundary : MonoBehaviour
{
    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.gameObject.SetActive(false);
    }
}
