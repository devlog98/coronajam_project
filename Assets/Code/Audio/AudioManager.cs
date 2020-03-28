using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {
    public static AudioManager instance; //static instance can be called any time

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    public void PlayAudioclip(string audioclip) {
        RuntimeManager.PlayOneShot(audioclip);
    }
}