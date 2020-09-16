using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScale : MonoBehaviour
{
    [Header("Settings to Button")]
    [SerializeField] private Text myText;
    [SerializeField] private float widthPT;
    [SerializeField] private float widthEn;
    private RectTransform width;
    // Start is called before the first frame update
    void Start()
    {
        width = this.GetComponent<RectTransform>();
        Size();   
    }

    private void Size()
    {
        //If temporario, se possivel obter a linguagem atual para utilizar no if
        if (myText.text == "VOLTAR PARA PARTIDA" || myText.text == "MENU PRINCIPAL")
        {
            width.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthPT);
        }
        else
        {
            width.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, widthEn);
        }
    }
}
