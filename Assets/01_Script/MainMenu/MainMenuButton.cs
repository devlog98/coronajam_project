using FMODUnity;
using Locallies.Tools;
using MSuits.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MaskableGraphic, IPointerEnterHandler, IPointerExitHandler {
    [Header("Audio Feedback")]
    [EventRef] [SerializeField] private string buttonHighlightSound = "event:/MPass";
    [EventRef] [SerializeField] private string buttonSelectSound = "event:/MSelect";

    [Header("Settings to change Text")]
    [SerializeField] private Text myText;
    [SerializeField] private int fontSize;
    [SerializeField] private int fontSizeExit;
    [SerializeField] private Color colorEnter;
    [SerializeField] private Color colorExit;

    public void OnPointerEnter(PointerEventData eventData) {
        if (!LanguageSelector.instance.IsLoading) {
            AudioManager.instance.PlayAudioclip(buttonHighlightSound); //play audio

            //change text
            if (myText != null) {
                StartCoroutine("FontIncrement");
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData) {
        if (!LanguageSelector.instance.IsLoading) {
            //change text
            if (myText != null) {
                StartCoroutine("FontDecrement");
            }
        }
    }

    public void OnClick() {
        if (!LanguageSelector.instance.IsLoading) {
            AudioManager.instance.PlayAudioclip(buttonSelectSound); //play audio

            //change text
            if (myText != null) {
                FontReturn();
            }
        }
    }

    private void FontReturn() {
        myText.fontSize = fontSizeExit;
        myText.color = colorExit;
    }

    private IEnumerator FontIncrement() {
        myText.color = colorEnter;
        for (int i = fontSizeExit; i <= fontSize; i++) {
            myText.fontSize = i;
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator FontDecrement() {
        myText.color = colorExit;
        for (int i = fontSize; i >= fontSizeExit; i--) {
            myText.fontSize = i;
            yield return new WaitForEndOfFrame();
        }
    }
}