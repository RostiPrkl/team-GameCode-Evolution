using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames.AudioController;

// Basic Shout Attack that inherits from attack controller

public class ShoutController : PlayerAttackController
{

    public SoundController shoutSound;

    protected override void Start()
    {
        shoutSound = FindObjectOfType<SoundController>();
        base.Start();
    }


    protected override void Attack()
    {
        if (attackData.AttackPrefab.name == "Bubbles")
        {
            shoutSound.PlaySoundFX("shoot01");
            //audioSource.PlayOneShot(audioSource.clip);
        }
        if (attackData.AttackPrefab.name == "Bubbles 2")
        {
            shoutSound.PlaySoundFX("shoot02");
            //audioSource.PlayOneShot(audioSource.clip);
        }
        if (attackData.AttackPrefab.name == "Bubble 3")
        {
            shoutSound.PlaySoundFX("shoot03");
            // audioSource.PlayOneShot(audioSource.clip);
        }
        if (attackData.AttackPrefab.name == "Vortex")
        {
            shoutSound.PlaySoundFX("bite03");
            // audioSource.PlayOneShot(audioSource.clip);
        }

        base.Attack();
        GameObject spawnedShout = Instantiate(attackData.AttackPrefab);
        spawnedShout.transform.position = transform.position;
        //spawnedShout.GetComponent<ShoutBehaviour>().DirectionCheck(player.lastMoveDirection);
    }
}
