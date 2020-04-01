using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MaskableGraphic, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Audio Feedback")]
    [EventRef] [SerializeField] private string buttonHighlightSound = "event:/MPass";
    [EventRef] [SerializeField] private string buttonSelectSound = "event:/MSelect";

    [Header("Settings to change Text")]
    [SerializeField] private Text myText;
    [SerializeField] private int fontSize;
    [SerializeField] private int fontSizeExit;
    [SerializeField] private Color colorEnter;
    [SerializeField] private Color colorExit;

    [Header("Settings to change Button")]
    [SerializeField] private GameObject myButton;
    [SerializeField] private Animator myButtonAnim;

    public void OnPointerEnter(PointerEventData eventData) {        
        AudioManager.instance.PlayAudioclip(buttonHighlightSound); //play audio

        //change text
        if (myText != null) {
            StartCoroutine("FontIncrement");
        }

        //change button
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //change text
        if (myText != null) {
            StartCoroutine("FontDecrement");
        }

        //change button
    }

    public void OnClick() {
        AudioManager.instance.PlayAudioclip(buttonSelectSound); //play audio

        //change text
        if (myText != null) {
            FontReturn();
        }

        //change button
    }

    #region Text Methods
    private void FontReturn() {
        myText.fontSize = fontSizeExit;
        myText.color = colorExit;
    }

    private IEnumerator FontIncrement()
    {
        myText.color = colorEnter;
        for (int i = fontSizeExit; i <= fontSize; i++ )
        {
            myText.fontSize = i;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FontDecrement()
    {
        myText.color = colorExit;
        for (int i = fontSize; i >= fontSizeExit; i--)
        {
            myText.fontSize = i;
            yield return new WaitForEndOfFrame();
        }
    }
    #endregion
}