using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour {
    [SerializeField] List<DialogueSentence> dialogueSentences; //list with all sentences from this dialogue
    [SerializeField] List<DialogueChoice> dialogueChoices; //list with all choices from this dialogue

    Queue<DialogueSentence> dialogueSentencesQueue; //queues to help with dialogue flow
    Queue<DialogueChoice> dialogueChoicesQueue;
    bool inSentence;

    //creates queue with all sentences from this dialogue
    private void Start() {
        dialogueSentencesQueue = new Queue<DialogueSentence>(dialogueSentences);
        dialogueChoicesQueue = new Queue<DialogueChoice>(dialogueChoices);
    }

    private void Update() {
        //if the sentence is over, show new sentence or quit dialogue
        if (!inSentence) {
            //if there are still sentences on queue
            if (dialogueSentencesQueue.Count > 0) {
                DialogueSentence sentence = dialogueSentencesQueue.Dequeue();
                StartCoroutine(ShowSentence(sentence));
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