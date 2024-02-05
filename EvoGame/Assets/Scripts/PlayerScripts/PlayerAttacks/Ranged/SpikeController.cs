using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeController : PlayerAttackController
{
   // public SoundController shoutEffects;
    protected override void Start()
    {
        // shoutEffects = FindObjectOfType<SoundController>();
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();

        GameObject spawnedSpike = Instantiate(attackData.AttackPrefab);
        spawnedSpike.transform.position = transform.position;

        //spawnedShout.GetComponent<ShoutBehaviour>().DirectionCheck(player.lastMoveDirection);
    }
}
