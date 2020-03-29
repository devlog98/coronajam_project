using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MaskableGraphic, IPointerEnterHandler, IPointerExitHandler
{
    [EventRef] [SerializeField] private string buttonHighlightSound = "event:/MPass";
    [EventRef] [SerializeField] private string buttonSelectSound = "event:/MSelect";
    [SerializeField] private Text myText;
    [SerializeField] private int fontSize;
    [SerializeField] private int fontSizeExit;
    [SerializeField] private Color colorEnter;
    [SerializeField] private Color colorExit;
    

    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.instance.PlayAudioclip(buttonHighlightSound);
        StartCoroutine("FontIncrement");
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        StartCoroutine("FontDecrement");
    }

    public void OnClick() {
        FontReturn();
        AudioManager.instance.PlayAudioclip(buttonSelectSound);
    }

    void FontReturn() {
        myText.fontSize = fontSizeExit;
        myText.color = colorExit;
    }

    IEnumerator FontIncrement()
    {       
        for (int i = fontSizeExit; i <= fontSize; i++ )
        {
            Debug.Log("incrementando");
            myText.fontSize = i;
            myText.color = colorEnter;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }

    IEnumerator FontDecrement()
    {
        for (int i = fontSize; i >= fontSizeExit; i--)
        {
            Debug.Log("tirando");
            myText.fontSize = i;
            myText.color = colorExit;
            yield return new WaitForEndOfFrame();
        }
        yield return new WaitForEndOfFrame();
    }
}