using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueChoice {
    [SerializeField] string choiceKey; //the key in order to localize choice
    [SerializeField] int choiceValue; //the value of choice (can harm your enemy or cure him)
    [SerializeField] string choiceReactionKey; //the key in order to localize reaction of enemy

    //getters
    public string ChoiceKey { get => choiceKey; }
    public int ChoiceValue { get => choiceValue; }
    public string ChoiceReactionKey { get => choiceReactionKey; }
}