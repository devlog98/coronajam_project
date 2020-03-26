using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyDifficulty {
    [SerializeField] private float speedSide;
    [SerializeField] private float speedUp;
    [SerializeField] private float timeMovement;
    [SerializeField] private float timeAttack;
    [SerializeField] private bool virusShot;

    //getters
    public float SpeedSide { get => speedSide; }
    public float SpeedUp { get => speedUp; }
    public float TimeMovement { get => timeMovement; }
    public float TimeAttack { get => timeAttack; }
    public bool VirusShot { get => virusShot; }
}