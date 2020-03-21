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
            DialogueSentence sentence = dialogueSentencesQueue.Dequeue();
            StartCoroutine(ShowSentence(sentence));
        }
    }

    //show sentence for a specific amount of time
    private IEnumerator ShowSentence(DialogueSentence sentence) {
        inSentence = true; //starting sentence
        Debug.Log(sentence.SentenceKey); //write sentence in screen
        yield return new WaitForSeconds(sentence.SentenceDuration); //wait for sentence duration to end
        inSentence = false; //stopping sentence
    }
}