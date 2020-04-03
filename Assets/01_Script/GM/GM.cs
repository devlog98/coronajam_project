using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GM : MonoBehaviour {
    public static GM instance; //static instance can be called any time

    [SerializeField] private Player player;
    [SerializeField] private UnwantedVisitor unwantedVisitor;
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

    public void LevelFailed(bool fromDialogue) {
        battle.LostBattle(); //disable all dialogue choices

        if (!fromDialogue) {
            //died from attack
            player.Die(ShowGameOver);
            unwantedVisitor.LockMove();
        }
        else {
            //died from dialogue
            unwantedVisitor.Die(ShowGameOver);
            player.LockMove();
        }

        Dialogue.instance.PauseDialogue(); //pauses battle so dialogue does not continue any further
        AudioManager.instance.MuteAudioManager(); //mute audio channel
        MusicManager.instance.StopEvent(); //mute music channel
    }

    //activates game over
    public void ShowGameOver(bool canShow) {
        if (canShow) {
            UI.instance.ShowGameOver(); 
        }
    }
}