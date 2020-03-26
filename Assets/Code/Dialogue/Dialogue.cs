using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    public static Dialogue instance;

    private Queue<DialogueSentence> dialogueSentencesQueue; //queues to help with dialogue flow
    private Queue<DialogueChoice> dialogueChoicesQueue;
    private Action<bool> dialogueCallback; //method to be called after dialogue is finished
    private bool inSentence;

    //setting singleton instance
    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        }
        else {
            instance = this;
        }
    }

    //creates queue with all sentences from this dialogue and sets callback
    public void StartDialogue(List<DialogueSentence> dialogueSentences, List<DialogueChoice> dialogueChoices, Action<bool> callback) {
        dialogueSentencesQueue = new Queue<DialogueSentence>(dialogueSentences);
        dialogueChoicesQueue = new Queue<DialogueChoice>(dialogueChoices);
        dialogueCallback = callback;
    }

    private void Update() {
        // if there is dialogue to be displayed
        if (dialogueSentencesQueue != null) {
            //go to next sentence if current is over
            if (!inSentence) {
                //show next sentence if queue has any left, else quit dialogue
                if (dialogueSentencesQueue.Count > 0) {
                    DialogueSentence sentence = dialogueSentencesQueue.Dequeue();
                    StartCoroutine(ShowSentence(sentence));
                }
                else {
                    dialogueSentencesQueue = null;
                    dialogueChoicesQueue = null;
                    dialogueCallback(true);
                }
            }
        }     
    }

    //show sentence for a specific amount of time
    private IEnumerator ShowSentence(DialogueSentence sentence) {
        inSentence = true; //starting sentence
        UI.instance.ShowDialogueSentence(sentence.SentenceKey); //write sentence in screen
        yield return new WaitForSeconds(sentence.SentenceDuration); //wait for sentence duration to end
        UI.instance.HideDialogueSentence(SentenceFadeCallback); //hide sentence after duration
    }

    //method will be called after sentence disappeared from screen
    public void SentenceFadeCallback(bool isFaded) {
        if (isFaded) {
            inSentence = false; //stopping sentence
        }
    }
}