using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedesSociais : MonoBehaviour
{
    [SerializeField] private string redeSocial;
    // Start is called before the first frame update

    public void openUrl()
    {
#if UNITY_WEBGL
        Application.ExternalEval("window.open('"+redeSocial+"','_blank')");
#endif

#if UNITY_STANDALONE
        Application.OpenURL(redeSocial);     
#endif

#if UNITY_ANDROID
        Application.OpenURL(redeSocial);
#endif


    }
}
