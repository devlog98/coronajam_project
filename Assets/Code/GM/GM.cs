using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static GM instance; //static instance can be called any time

    [SerializeField] private Battle battle;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void LevelCompleted() {

    }

    public void LevelFailed(float animationLength) {
        battle.DisableAllChoices(); //disable all dialogue choices

        Dialogue.instance.PauseDialogue(); //pauses battle so dialogue does not continue any further
        AudioManager.instance.MuteAudioManager(); //mute audio channel
        MusicManager.instance.StopEvent(); //mute music channel

        StartCoroutine(WaitForDeathAnimation(animationLength));
    }

    private IEnumerator WaitForDeathAnimation(float length) {
        yield return new WaitForSeconds(length);       
        UI.instance.ShowGameOver(); //activates game over
    }
}