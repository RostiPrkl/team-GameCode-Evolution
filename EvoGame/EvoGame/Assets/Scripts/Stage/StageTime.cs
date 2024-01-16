using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//responsible for tracking the elapsed time in a stage and updating a timer UI.
public class StageTime : MonoBehaviour
{
    public float time;
    TimerUI timerUI;

    private void Awake()
    {
        timerUI = FindObjectOfType<TimerUI>();  //finds the first active object of type TimerUI in the scene.
    }

    private void Update()
    {
        time += Time.deltaTime;                 //The time variable is updated by adding Time.deltaTime.
        timerUI.UpdateTime(time);               //Responsible for displaying or updating the timer in the user interface.
    }

}