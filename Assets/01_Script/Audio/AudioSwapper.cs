using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSwapper : MonoBehaviour {
    [SerializeField] private UnwantedVisitor unwantedVisitor;
    [EventRef] [SerializeField] private string swapSound;

    private void OnMouseDown() {
        unwantedVisitor.SwapDeathSound(swapSound);
    }
}