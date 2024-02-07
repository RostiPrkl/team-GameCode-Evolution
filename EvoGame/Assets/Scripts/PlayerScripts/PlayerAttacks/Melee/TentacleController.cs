using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TentacleController : PlayerAttackController
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
            case "Tentacle 0":
                attackSound.PlayEffect(34);
                break;
            case "Tentacle 1":
                attackSound.PlayEffect(35);
                break;
            case "Tentacle 2":
                attackSound.PlayEffect(36);
                break;
            case "Tentacle 3":
                attackSound.PlayEffect(37);
                break;
        }

        GameObject spawnedTentacle = Instantiate(attackData.AttackPrefab);
        spawnedTentacle.transform.SetParent(transform);
        spawnedTentacle.transform.position = transform.position;
    }
}
