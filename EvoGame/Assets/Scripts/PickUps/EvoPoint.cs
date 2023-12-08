using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoPoint : Pickup, ICollectible
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


    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExp(experienceCollected);
    }
}
