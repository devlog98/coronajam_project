﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChoicesController : MonoBehaviour
{
    public Battle BattleScript;

    public void FinishRound(int choiceIndex)
    {
        BattleScript.FinishRound(choiceIndex);
    }
}
