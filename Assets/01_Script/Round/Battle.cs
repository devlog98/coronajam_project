using Locallies.Core;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Battle : MonoBehaviour {
    [Header("Rounds")]
    [SerializeField] private List<Round> rounds; //list with all rounds sequentially ordered for the battle
    [SerializeField] private UnwantedVisitor enemy; //reference of enemy

    [Header("PostProcessing")]
    UnityEngine.Rendering.Universal.ColorAdjustments colorAdjustments;
    [SerializeField] GameObject volume;
    private VolumeProfile volumeProfile;

    [Header("Choices")]
    [SerializeField] private List<GameObject> choiceBoxes;
    private List<TextMeshProUGUI> choiceTexts = new List<TextMeshProUGUI>();

    private DialogueChoice[] choiceArray;
    private int choiceIndex;
    [HideInInspector] public bool reactionWasShown;
    private bool isFirstDialogue;
    private bool inReaction;
    public GameObject player;

    private Queue<Round> roundsQueue; //queues to help with battle flow
    public bool inRound;
    public Player playerScript;

    float t;

    bool fadeIn;
    public float duration = 0.2f;
    private bool lostBattle;

    //creates queue with all rounds from this battle
    private void Start() {
        roundsQueue = new Queue<Round>(rounds);

        foreach (GameObject choiceBox in choiceBoxes) {
            choiceTexts.Add(choiceBox.GetComponentInChildren<TextMeshProUGUI>());
        }

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

    private void Update() {
        if (colorAdjustments.saturation.value <= -100) {
            fadeIn = false;
        }
        if (fadeIn) {
            t += Time.deltaTime / duration;

            colorAdjustments.saturation.Override(Mathf.Lerp(0, -100, t));
        }

        //activate round if none is happening
        if (!inRound && !lostBattle) {
            DisableAllChoices();

            if (!isFirstDialogue && !inReaction) {
                StartCoroutine(Dialogue.instance.ShowReaction(choiceArray[choiceIndex]));
                inReaction = true;
            }

            if (roundsQueue.Count > 0 && reactionWasShown) {
                Round round = roundsQueue.Dequeue(); //grabs next round
                Dialogue.instance.StartDialogue(round.DialogueSentences, round.DialogueChoices, DialogueOverCallback); //sends all needed data to dialogue
                enemy.SetDifficulty(round.EnemyDifficulty); //sends all needed data to enemy
                inRound = true;
                isFirstDialogue = false;
            }
            else {
                MusicManager.instance.StopEvent();
                SceneManager.LoadScene(3);
            }
        }
    }

    //method will be called after round dialogue is over
    public void DialogueOverCallback(bool isOver, Queue<DialogueChoice> choices) {
        choiceArray = choices.ToArray();
        fadeIn = true;
        Time.timeScale = 0.3f;
        MusicManager.instance.SetMusicState("Health", 1); //change music when time is slowed down

        for (int i = 0; i < choices.Count; i++) {
            choiceBoxes[i].SetActive(true);
            choiceTexts[i].text = LocalizationManager.Localize(choiceArray[i].ChoiceKey);
        }

        if (isOver && false) {
            inRound = false; //stopping round
        }
    }

    public void FinishRound(int choiceIndexp) {
        this.choiceIndex = choiceIndexp;
        StartCoroutine(backToNormalColor());

        inRound = false;
        inReaction = false;

        Time.timeScale = 1;
        MusicManager.instance.SetMusicState("Health", 3); //returns music when time is restored
    }

    public void DisableAllChoices() {
        foreach (GameObject choiceBox in choiceBoxes) {
            choiceBox.SetActive(false);
        }
    }

    IEnumerator backToNormalColor() {
        if (choiceArray[choiceIndex].ChoiceValue == 0) {
            colorAdjustments.saturation.Override(0);
        }
        else {
            playerScript.ReceiveDamageFromDialogue(1);
            colorAdjustments.saturation.Override(100);
            yield return new WaitForSeconds(0.3f);
            colorAdjustments.saturation.Override(0);
        }
    }

    public void LostBattle() {
        lostBattle = true;
        DisableAllChoices();
        Time.timeScale = 1.0f;
        colorAdjustments.saturation.Override(0);
    }
}