using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainMenuButton : MaskableGraphic, IPointerEnterHandler {
    [EventRef] [SerializeField] private string buttonHighlightSound = "event:/MPass";
    [EventRef] [SerializeField] private string buttonSelectSound = "event:/MSelect";

    public void OnPointerEnter(PointerEventData eventData) {
        AudioManager.instance.PlayAudioclip(buttonHighlightSound);
    }

    public void OnClick() {
        AudioManager.instance.PlayAudioclip(buttonSelectSound);
    }
}