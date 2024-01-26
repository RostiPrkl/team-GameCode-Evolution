using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : Pickup, ICollectible
{
    public int healthCollected;

    protected override void Start()
    {
        base.Start();
    }


    protected override void Update()
    {
        base.Update();
    }


    public override void Collect()
    {
        if (isCollected)
            return;
        else
        base.Collect();

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthCollected);
    }
}
