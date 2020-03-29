﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {
    public static Pause instance; //static instance can be called any time
    bool isPaused;

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
            Time.timeScale = 0.0f; //stops Time completely
            UI.instance.ShowPause();
            isPaused = true;
        }
        else {
            //unpause
            Time.timeScale = 1.0f; //resumes Time
            UI.instance.HidePause();
            isPaused = false;
        }
    }
}