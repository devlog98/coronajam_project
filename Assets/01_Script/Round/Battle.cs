using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Battle : MonoBehaviour
{
    [SerializeField] private List<Round> rounds; //list with all rounds sequentially ordered for the battle
    [SerializeField] private UnwantedVisitor enemy; //reference of enemy

    [Header("PostProcessing")]
    UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    public VolumeProfile volumeProfile;
    [SerializeField] GameObject volume;

    [Header("Choices")]
    [SerializeField] private GameObject choice1, choice2, choice3, choice4, choice5;
    [HideInInspector] public TextMeshProUGUI choice1Txt, choice2Txt, choice3Txt, choice4Txt, choice5Txt;
    private DialogueChoice[] choiceArray;
    private int choiceIndex;
    [HideInInspector] public bool reactionWasShown;
    private bool isFirstDialogue;
    private bool inReaction;
    public GameObject player;

    private GameObject[] pausableGameObjects;

    private Queue<Round> roundsQueue; //queues to help with battle flow
    public bool inRound;
    public Player playerScript;

    float t;

    bool fadeIn;
    public float duration = 0.2f;
    //creates queue with all rounds from this battle
    private void Start()
    {
        roundsQueue = new Queue<Round>(rounds);
        pausableGameObjects = GameObject.FindGameObjectsWithTag("Pausable");

        choice1Txt = choice1.GetComponentInChildren<TextMeshProUGUI>();
        choice2Txt = choice2.GetComponentInChildren<TextMeshProUGUI>();
        choice3Txt = choice3.GetComponentInChildren<TextMeshProUGUI>();
        choice4Txt = choice4.GetComponentInChildren<TextMeshProUGUI>();
        choice5Txt = choice5.GetComponentInChildren<TextMeshProUGUI>();

        t = 0;
        fadeIn = false;
        isFirstDialogue = true;
        inReaction = false;
        reactionWasShown = true;
        volumeProfile = volume.GetComponent<UnityEngine.Rendering.Volume>()?.profile;
        if (!volumeProfile) throw new System.NullReferenceException(nameof(UnityEngine.Rendering.VolumeProfile));

        if (!volumeProfile.TryGet(out colorAdjustments)) throw new System.NullReferenceException(nameof(colorAdjustments));

        playerScript = player.GetComponent<Player>();
    }

    private void Update()
    {

        if (colorAdjustments.saturation.value <= -100)
        {
            fadeIn = false;
        }
        if (fadeIn)
        {
            t += Time.deltaTime / duration;

            colorAdjustments.saturation.Override(Mathf.Lerp(0, -100, t));
        }
        //activate round if none is happening
        if (!inRound)
        {

            DisableAllChoices();
            EnablePausableScripts();

            if (!isFirstDialogue && !inReaction)
            {
                StartCoroutine(Dialogue.instance.ShowReaction(choiceArray[choiceIndex]));
                inReaction = true;
            }

            if (roundsQueue.Count > 0 && reactionWasShown)
            {

                Round round = roundsQueue.Dequeue(); //grabs next round
                Dialogue.instance.StartDialogue(round.DialogueSentences, round.DialogueChoices, DialogueOverCallback); //sends all needed data to dialogue
                enemy.SetDifficulty(round.EnemyDifficulty); //sends all needed data to enemy
                inRound = true;
                isFirstDialogue = false;
            }
            else
            {
                MusicManager.instance.StopEvent();
                SceneManager.LoadScene(3);
            }
        }
    }

    //method will be called after round dialogue is over
    public void DialogueOverCallback(bool isOver, Queue<DialogueChoice> Choices)
    {

        choiceArray = Choices.ToArray();
        fadeIn = true;
        Time.timeScale = 0.3f;

        MonoBehaviour[] scripts = player.GetComponents<MonoBehaviour>();


        foreach (MonoBehaviour script in scripts)
        {
            script.enabled = false;
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


        if (isOver && false)
        {
            inRound = false; //stopping round

        }
    }

    public void FinishRound(int choiceIndexp)
    {
        inRound = false;
        inReaction = false;
        this.choiceIndex = choiceIndexp;
        Time.timeScale = 1;
        StartCoroutine(backToNormalColor());

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

    IEnumerator backToNormalColor()
    {
        if (choiceArray[choiceIndex].ChoiceValue == 0)
        {
            colorAdjustments.saturation.Override(0);
        }
        else
        {
            playerScript.ReceiveDamage(1);
            colorAdjustments.saturation.Override(100);
            yield return new WaitForSeconds(0.3f);
            colorAdjustments.saturation.Override(0);
        }
    }
}