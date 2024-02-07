using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShoutController : PlayerAttackController
{

    protected override void Start()
    {
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
