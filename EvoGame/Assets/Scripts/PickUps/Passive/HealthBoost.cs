using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.currentHealth *= 1 + passiveItem.Multiplier / 100f;
    }
}
