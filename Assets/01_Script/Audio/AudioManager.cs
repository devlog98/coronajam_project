using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance; //static instance can be called any time
    private bool isMute; //if manager is mute or not

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

    //controls if audio is played or not
    public void MuteAudioManager() {
        isMute = true;
    }

    public void UnmuteAudioManager() {
        isMute = false;
    }
}