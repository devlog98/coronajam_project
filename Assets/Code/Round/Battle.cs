using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battle : MonoBehaviour {
    [SerializeField] private List<Round> rounds; //list with all rounds sequentially ordered for the battle
    [SerializeField] private UnwantedVisitor enemy; //reference of enemy

    private Queue<Round> roundsQueue; //queues to help with battle flow
    private bool inRound;

    //creates queue with all rounds from this battle
    private void Start() {
        roundsQueue = new Queue<Round>(rounds);
    }

    private void Update() {
        //activate round if none is happening
        if (!inRound) {
            if (roundsQueue.Count > 0) {
                Round round = roundsQueue.Dequeue(); //grabs next round
                Dialogue.instance.StartDialogue(round.DialogueSentences, round.DialogueChoices, DialogueOverCallback); //sends all needed data to dialogue
                enemy.SetDifficulty(round.EnemyDifficulty); //sends all needed data to enemy
                inRound = true;
            }
            else {
                //BATTLE IS OVER
            }
        }
    }

    //method will be called after round dialogue is over
    public void DialogueOverCallback(bool isOver) {
        if (isOver) {
            inRound = false; //stopping round
        }
    }
}