using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance; //static instance can be called any time
    private bool isMute; //if manager is mute or not
    EventInstance audioEvent;

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    //plays FMOD clip
    public void PlayAudioclip(string audioclip) {
        if (!isMute) {
            RuntimeManager.PlayOneShot(audioclip);
        }
    }

    //plays FMOD event (for tutorial)
    public void PlayAudioclipEvent(string audioclip) {
        if (!String.IsNullOrEmpty(audioclip)) {
            if (!isMute) {
                audioEvent = RuntimeManager.CreateInstance(audioclip);
                audioEvent.start();
                audioEvent.release();
            }
        }
    }

    //stops FMOD clips
    public void StopAudioclips() {
        audioEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    //controls if audio is played or not
    public void MuteAudioManager() {
        isMute = true;
    }

    public void UnmuteAudioManager() {
        isMute = false;
    }
}