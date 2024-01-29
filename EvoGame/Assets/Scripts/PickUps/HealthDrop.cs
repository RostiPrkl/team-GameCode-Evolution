using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : Pickup, ICollectible
{
    public int healthCollected;
    public AudioManager heathPointSound;

    protected override void Start()
    {
        heathPointSound = FindObjectOfType<AudioManager>();
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

        if (heathPointSound.IsSoundPlaying(11) == false)
        {
            heathPointSound.PlayEffect(11);
        }

        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthCollected);
    }
}
