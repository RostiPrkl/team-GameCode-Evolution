using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoPoint : Pickup
{
    public int experienceCollected;

    public AudioManager evoPointSound;

    protected override void Start()
    {
        base.Start();
        evoPointSound = FindObjectOfType<AudioManager>();
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
        {
            if (evoPointSound.IsSoundPlaying(11) == false)
            {
                evoPointSound.PlayEffect(11);
            }
            base.Collect();
           
        }
        
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExp(experienceCollected);
    }
}
