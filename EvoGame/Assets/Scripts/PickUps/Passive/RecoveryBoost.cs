using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecoveryBoost : PassiveItem
{
    protected override void Modifier()
    {
        player.CurrentRecovery = 1f;
        player.CurrentRecovery *= 1 + passiveItem.Multiplier / 100f;
    }
}
