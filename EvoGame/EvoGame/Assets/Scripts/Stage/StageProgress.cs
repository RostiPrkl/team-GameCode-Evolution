using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//responsible for calculating the progress of a stage based on the elapsed time.
public class StageProgress : MonoBehaviour
{
    StageTime stageTime;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();                                  //retrieves the StageTime component attached to the same GameObject.
    }

    [SerializeField] float progressTimeRate = 30f;                              //indicating the rate at which progress increases over time. 
    [SerializeField] float progressPerSplit = 0.2f;                             //indicating the amount of progress added per split.

    //that calculates and returns the current progress of the stage.
    public float Progress
    {
        get
        {
            return 1f + stageTime.time / progressTimeRate * progressPerSplit;   //The calculation is designed to provide a gradual increase in progress over time.
        }
    }

}