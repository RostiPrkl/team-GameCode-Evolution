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

        switch (attackData.AttackPrefab.name.ToString())
        {
            case "ShoutWeapon 0":
                attackSound.PlayEffect(21);
                break;
            case "ShoutWeapon 1":
                attackSound.PlayEffect(22);
                break;
            case "ShoutWeapon 2":
                attackSound.PlayEffect(23);
                break;
            case "ShoutWeapon 3":
                attackSound.PlayEffect(27);
                break;
        }

        GameObject spawnedShout = Instantiate(attackData.AttackPrefab);
        spawnedShout.transform.position = transform.position;

        //spawnedShout.GetComponent<ShoutBehaviour>().DirectionCheck(player.lastMoveDirection);
    }
}
