using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiteController : PlayerAttackController
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedBite = Instantiate(attackData.AttackPrefab);
        spawnedBite.transform.SetParent(transform);
        spawnedBite.transform.position = transform.position;
    }
}
