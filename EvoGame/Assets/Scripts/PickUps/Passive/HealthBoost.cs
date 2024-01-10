using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.newMaxHealth *= 1 + passiveItem.Multiplier / 100f;
    }
}
