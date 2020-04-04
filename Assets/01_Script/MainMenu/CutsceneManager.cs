using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {
    [SerializeField] private List<CutsceneSlide> cutscenes;
    public int sceneIndex;
    private int slideIndex = 0;

    public Image currentImage;
    public Text currentName;
    public Text currentSentence;

    public GameObject nameContainer;
    public GameObject sentenceContainer;

    private void Start() {
        ShowCutscene();
    }

    public void NextImageCutscene() {
        slideIndex++;

        if (slideIndex < cutscenes.Count) {
            ShowCutscene();
        }
        else {
            JumpIntro();
        }
    }

    public void JumpIntro() {
        AudioManager.instance.StopAudioclips();
        MusicManager.instance.StopEvent();
        SceneManager.LoadScene(sceneIndex);
    }

    private void ShowCutscene() {
        currentImage.sprite = cutscenes[slideIndex].Sprite; //change cutscene sprite
        AudioManager.instance.StopAudioclips();
        AudioManager.instance.PlayAudioclipEvent(cutscenes[slideIndex].SoundEffect); //play specific audioclip for cutscene

        //show or hide name box depending on value
        if (String.IsNullOrEmpty(cutscenes[slideIndex].Name)) {
            nameContainer.SetActive(false);
        }
        else {
            nameContainer.SetActive(true);
            currentName.text = cutscenes[slideIndex].Name;
        }

        //show or hide dialogue box depending on value
        if (String.IsNullOrEmpty(cutscenes[slideIndex].Sentence)) {
            sentenceContainer.SetActive(false);
        }
        else {
            sentenceContainer.SetActive(true);
            currentSentence.text = cutscenes[slideIndex].Sentence;
        }
    }
}