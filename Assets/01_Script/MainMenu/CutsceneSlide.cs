using FMODUnity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CutsceneSlide {
    [SerializeField] private Sprite sprite;
    [SerializeField] private string name;
    [SerializeField] private string sentence;
    [SerializeField] [EventRef] private string soundEffect;

    public Sprite Sprite { get => sprite; }
    public string Name { get => name; }
    public string Sentence { get => sentence; }
    public string SoundEffect { get => soundEffect; }
}