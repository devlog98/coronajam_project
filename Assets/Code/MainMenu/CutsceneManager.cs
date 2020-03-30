using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CutsceneManager : MonoBehaviour {
    [SerializeField] private List<CutsceneSlide> cutscenes;
    private int slideIndex = 0;

    public Image currentImage;
    public Text currentName;
    public Text currentSentence;

    public GameObject nameContainer;
    public GameObject sentenceContainer;

    private void Start() {
        currentImage.sprite = cutscenes[0].Sprite;
        AudioManager.instance.PlayAudioclip(cutscenes[0].SoundEffect);

        if (String.IsNullOrEmpty(cutscenes[0].Name)) {
            nameContainer.SetActive(false);
        }
        else {
            nameContainer.SetActive(true);
            currentName.text = cutscenes[0].Name;
        }

        if (String.IsNullOrEmpty(cutscenes[0].Sentence)) {
            sentenceContainer.SetActive(false);
        }
        else {
            sentenceContainer.SetActive(true);
            currentSentence.text = cutscenes[0].Sentence;
        }
    }

    public void NextImageCutscene() {
        slideIndex++;

        if (slideIndex < cutscenes.Count) {
            currentImage.sprite = cutscenes[slideIndex].Sprite;
            AudioManager.instance.PlayAudioclip(cutscenes[slideIndex].SoundEffect);

            if (String.IsNullOrEmpty(cutscenes[slideIndex].Name)) {
                nameContainer.SetActive(false);
            } else {
                nameContainer.SetActive(true);
                currentName.text = cutscenes[slideIndex].Name;
            }

            if (String.IsNullOrEmpty(cutscenes[slideIndex].Sentence)) {
                sentenceContainer.SetActive(false);
            }
            else {
                sentenceContainer.SetActive(true);
                currentSentence.text = cutscenes[slideIndex].Sentence;
            }
        }
        else {
            JumpIntro();
        }
    }

    public void JumpIntro() {
        AudioManager.instance.PlayAudioclip("Teste");
        SceneManager.LoadScene(1);
    }
}