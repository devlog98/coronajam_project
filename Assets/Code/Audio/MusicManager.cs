using FMOD.Studio;
using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public static MusicManager instance; //static instance can be called any time
    [EventRef] public string music;
    EventInstance musicEvent;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;          
            instance.StartEvent();
        }
    }

    void StartEvent() {
        musicEvent = RuntimeManager.CreateInstance(music);
        musicEvent.start();
    }

    public void StopEvent() {
        musicEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    public void SetMusicState(string parameter, int state) {
        musicEvent.setParameterByName(parameter, state);
    }
}