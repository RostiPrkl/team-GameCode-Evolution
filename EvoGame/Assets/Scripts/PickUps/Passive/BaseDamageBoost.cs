using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.currentBaseDamage *= 1 + passiveItem.Multiplier / 100f;
    }
}
