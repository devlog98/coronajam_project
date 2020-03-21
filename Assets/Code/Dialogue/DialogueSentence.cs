using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueSentence {
    [SerializeField] string sentenceKey; //the key used in order to localize sentence
    [SerializeField] float sentenceDuration; //the duration of sentence in seconds

    //getters
    public string SentenceKey { get => sentenceKey; }
    public float SentenceDuration { get => sentenceDuration; }
}