using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Round {
    [SerializeField] private List<DialogueSentence> dialogueSentences; //list with all sentences from this round    
    [SerializeField] private EnemyDifficulty enemyDifficulty; //controls enemy variables in this round
    [SerializeField] private List<DialogueChoice> dialogueChoices; //list with all choices from this round

    //getters
    public List<DialogueSentence> DialogueSentences { get => dialogueSentences; }
    public EnemyDifficulty EnemyDifficulty { get => enemyDifficulty; }
    public List<DialogueChoice> DialogueChoices { get => dialogueChoices; }
}