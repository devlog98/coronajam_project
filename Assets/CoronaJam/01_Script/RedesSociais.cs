using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedesSociais : MonoBehaviour
{
    [SerializeField] private string redeSocial;
    // Start is called before the first frame update

    public void openUrl()
    {
        Application.OpenURL(redeSocial);
    }
}
