using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.CurrentMovementSpeed *= 1 + passiveItem.Multiplier / 100f;
    }


    protected override void Update()
    {
        // if (Input.GetKeyDown(KeyCode.CapsLock))
        // {
        //    float dashSpeed = player.CurrentMovementSpeed * 5;
        // }
    }
}
