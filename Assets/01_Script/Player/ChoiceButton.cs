using FMODUnity;
using MSuits.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChoiceButton : MaskableGraphic, IPointerEnterHandler, IPointerExitHandler {
    [Header("Audio Feedback")]
    [EventRef] [SerializeField] private string buttonHighlightSound = "event:/MPass";
    [EventRef] [SerializeField] private string buttonSelectSound = "event:/MSelect";

    [Header("Settings to change Button")]
    [SerializeField] private RectTransform myButton;
    [SerializeField] private float buttonScale;
    [SerializeField] private float buttonScaleExit;

    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.instance.PlayAudioclip(buttonHighlightSound); //play audio
       
        if (myButton != null) {//change button
            StartCoroutine("ButtonIncrement");
        }
    }

    public void OnPointerExit(PointerEventData eventData) {     
        if (myButton != null) {//change button
            StartCoroutine("ButtonDecrement");
        }
    }

    public void OnClick() {
        AudioManager.instance.PlayAudioclip(buttonSelectSound); //play audio
       
        if (myButton != null) {//change button
            ButtonReturn();
        }
    }
    private IEnumerator ButtonIncrement() {
        float scale = myButton.localScale.x; //checks only the x axis because all axis are equal

        while (scale <= buttonScale) {
            scale += 0.1f;
            myButton.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator ButtonDecrement() {
        float scale = myButton.localScale.x; //checks only the x axis because all axis are equal

        while (scale >= buttonScaleExit) {
            scale -= 0.1f;
            myButton.localScale = new Vector3(scale, scale, scale);
            yield return new WaitForEndOfFrame();
        }
    }

    private void ButtonReturn() {
        myButton.localScale = new Vector3(buttonScaleExit, buttonScaleExit, buttonScaleExit);
    }
}