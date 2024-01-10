using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoPoint : Pickup
{
    public int experienceCollected;


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
        player.IncreaseExp(experienceCollected);
    }
}
