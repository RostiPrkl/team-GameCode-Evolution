using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.currentMovementSpeed *= 1 + passiveItem.Multiplier / 100f;
    }
}
