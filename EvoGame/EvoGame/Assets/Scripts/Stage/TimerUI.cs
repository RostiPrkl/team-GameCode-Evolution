using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//responsible for updating a UI element with the elapsed time in a specific format. 
public class TimerUI : MonoBehaviour
{
    TextMeshProUGUI text;                                               //meant to update a text-based UI element.

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();                         //Reference the TextMeshProUGUI component on the same GameObject where this script is attached.
    }

    public void UpdateTime(float time)
    {
        int minutes = (int)(time / 60f);                                //Calculates the number of minutes by dividing the elapsed time by 60 and casting the result to an integer.
        int seconds = (int)(time % 60f);                                //Calculates the remaining seconds by taking the modulo of the elapsed time with 60 and casting the result to an integer.

        text.text = minutes.ToString() + ":" + seconds.ToString("00");  //Formats the minutes and seconds as a string and assigns it to the text.text property of the TextMeshProUGUI component.
    }

}