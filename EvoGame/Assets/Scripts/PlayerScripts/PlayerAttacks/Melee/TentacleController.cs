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
        GameObject spawnedTentacle = Instantiate(attackData.AttackPrefab);
        spawnedTentacle.transform.SetParent(transform);
        spawnedTentacle.transform.position = transform.position;
    }
}
