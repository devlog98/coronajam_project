using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EnemyDifficulty {
    [SerializeField] private float speedSide; //speed in which enemy moves horizontally
    [SerializeField] private float speedUp; //speed in which enemy moves vertically
    [SerializeField] private float timeMovement; //duration between movements
    [SerializeField] private float timeVirusShot; //duration between virus shot attack
    [SerializeField] private float timeSneeze; //duration between virus sneeze attack
    [SerializeField] private bool virusShot; //toggle specific attacks from enemy
    [SerializeField] private bool virusSneeze;

    //getters
    public float SpeedSide { get => speedSide; }
    public float SpeedUp { get => speedUp; }
    public float TimeMovement { get => timeMovement; }
    public float TimeVirusShot { get => timeVirusShot; }
    public bool VirusShot { get => virusShot; }
    public float TimeSneeze { get => timeSneeze; }
    public bool VirusSneeze { get => virusSneeze; }
}