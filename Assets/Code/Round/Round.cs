using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Round {
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private EnemyDifficulty enemyDifficulty;

    public Dialogue Dialogue { get => dialogue; }
    public EnemyDifficulty EnemyDifficulty { get => enemyDifficulty; }
}