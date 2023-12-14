using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseDamageBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.CurrentBaseDamage *= 1 + passiveItem.Multiplier / 100f;
    }
}
