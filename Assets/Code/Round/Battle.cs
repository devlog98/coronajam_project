using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class Battle : MonoBehaviour {
    [SerializeField] private List<Round> rounds; //list with all rounds sequentially ordered for the battle
    [SerializeField] private UnwantedVisitor enemy; //reference of enemy

    [Header("Choices")]
    [SerializeField] private GameObject choice1, choice2, choice3, choice4, choice5;
    public TextMeshProUGUI choice1Txt, choice2Txt, choice3Txt, choice4Txt, choice5Txt;



    private GameObject[] pausableGameObjects;

    private Queue<Round> roundsQueue; //queues to help with battle flow
    public  bool inRound;

    //creates queue with all rounds from this battle
    private void Start() {
        roundsQueue = new Queue<Round>(rounds);
        pausableGameObjects = GameObject.FindGameObjectsWithTag("Pausable");

        choice1Txt = choice1.GetComponentInChildren<TextMeshProUGUI>();
        choice2Txt = choice2.GetComponentInChildren<TextMeshProUGUI>();
        choice3Txt = choice3.GetComponentInChildren<TextMeshProUGUI>();
        choice4Txt = choice4.GetComponentInChildren<TextMeshProUGUI>();
        choice5Txt = choice5.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update() {
        //activate round if none is happening
        if (!inRound) {
            
            DisableAllChoices();
            EnablePausableScripts();

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
    public void DialogueOverCallback(bool isOver, Queue<DialogueChoice> Choices) {

        
       
        foreach (GameObject go in pausableGameObjects)
        {
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();
            
            foreach(MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }

        switch (Choices.Count)
        {
            case 1:
                choice1Txt.text = Choices.Dequeue().ChoiceKey;
                choice1.SetActive(true);
                break;

            case 2:

                choice1Txt.text = Choices.Dequeue().ChoiceKey;
                choice1.SetActive(true);


                choice2Txt.text = Choices.Dequeue().ChoiceKey;
                choice2.SetActive(true);
                
                break;

            case 3:
                choice1Txt.text = Choices.Dequeue().ChoiceKey;
                choice1.SetActive(true);

                choice2Txt.text = Choices.Dequeue().ChoiceKey;
                choice2.SetActive(true);

                choice3Txt.text = Choices.Dequeue().ChoiceKey;
                choice3.SetActive(true);
                break;

            case 4:
                choice1Txt.text = Choices.Dequeue().ChoiceKey;
                choice1.SetActive(true);

                choice2Txt.text = Choices.Dequeue().ChoiceKey;
                choice2.SetActive(true);

                choice3Txt.text = Choices.Dequeue().ChoiceKey;
                choice3.SetActive(true);

                choice4Txt.text = Choices.Dequeue().ChoiceKey;
                choice4.SetActive(true);
                break;

            case 5:
                choice1Txt.text = Choices.Dequeue().ChoiceKey;
                choice1.SetActive(true);

                choice2Txt.text = Choices.Dequeue().ChoiceKey;
                choice2.SetActive(true);

                choice3Txt.text = Choices.Dequeue().ChoiceKey;
                choice3.SetActive(true);

                choice4Txt.text = Choices.Dequeue().ChoiceKey;
                choice4.SetActive(true);

                choice5Txt.text = Choices.Dequeue().ChoiceKey;
                choice5.SetActive(true);
                break;

        }


        if (isOver && false) {
            inRound = false; //stopping round

        }
    }

    public  void FinishRound()
    {
        inRound = false;
    }

    public void DisableAllChoices()
    {
        choice1.SetActive(false);
        choice2.SetActive(false);
        choice3.SetActive(false);
        choice4.SetActive(false);
        choice5.SetActive(false);
    }

    public void EnablePausableScripts()
    {
        foreach (GameObject go in pausableGameObjects)
        {
            MonoBehaviour[] scripts = go.GetComponents<MonoBehaviour>();

            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = true;
            }
        }
    }
}