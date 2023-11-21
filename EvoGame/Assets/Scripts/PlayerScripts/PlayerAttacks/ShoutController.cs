using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Basic Shout Attack that inherits from attack controller

public class ShoutController : PlayerAttackController
{

    protected override void Start()
    {
        base.Start();
    }


    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedShout = Instantiate(attackPrefab);
        spawnedShout.transform.position = transform.position;
        spawnedShout.GetComponent<ShoutBehaviour>().DirectionCheck(player.lastMoveDirection);
    }
}
