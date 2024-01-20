using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames.AudioController;

public class EvoPoint : Pickup
{
    public int experienceCollected;
    public SoundController sndCntrl;

    protected override void Start()
    {
        sndCntrl = FindObjectOfType<SoundController>();
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
        {
            sndCntrl.PlaySoundFX("experience2");
            base.Collect();
           
        }
        
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExp(experienceCollected);
    }
}
