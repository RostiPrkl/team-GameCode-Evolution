using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Gaskellgames.AudioController;

// Basic Shout Attack that inherits from attack controller

public class ShoutController : PlayerAttackController
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

        GameObject spawnedShout = Instantiate(attackData.AttackPrefab);
        spawnedShout.transform.position = transform.position;

        //spawnedShout.GetComponent<ShoutBehaviour>().DirectionCheck(player.lastMoveDirection);
    }
}
