using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyDifficulty {
    [SerializeField] private float speedSide; //speed in which enemy moves horizontally
    [SerializeField] private float speedUp; //speed in which enemy moves vertically
    [SerializeField] private float timeMovement; //duration between movements
    [SerializeField] private float timeAttack; //duration between attacks
    [SerializeField] private bool virusShot; //toggle specific attacks from enemy

    //getters
    public float SpeedSide { get => speedSide; }
    public float SpeedUp { get => speedUp; }
    public float TimeMovement { get => timeMovement; }
    public float TimeAttack { get => timeAttack; }
    public bool VirusShot { get => virusShot; }
}