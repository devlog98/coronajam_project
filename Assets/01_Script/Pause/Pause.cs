using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    public static Pause instance; //static instance can be called any time
    bool isPaused;
    float currentTimeScale;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    //when player pauses the game
    public void TogglePause() {        
        if (!isPaused) {
            //pause
            currentTimeScale = Time.timeScale;
            Time.timeScale = 0.0f; //stops Time completely
            UI.instance.ShowPause();
            MusicManager.instance.SetMusicState("Pause", 1);
            isPaused = true;
        }
        else {
            //unpause
            Time.timeScale = currentTimeScale; //resumes Time
            UI.instance.HidePause();
            MusicManager.instance.SetMusicState("Pause", 0);
            isPaused = false;
        }
    }
}