using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.UI;
using UnityEngine;
using UnityEngine.Rendering;

//Base for all player attack types to inherit from

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Stats")]
    public GameObject attackPrefab;
    public float damage;
    public float speed;
    public float cooldown;
    protected Player player;
    float currentCooldown;



    protected virtual void Start()
    {
        player = FindObjectOfType<Player>();
        currentCooldown = cooldown;
    }


    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
            Attack();
    }


    protected virtual void Attack()
    {
        currentCooldown = cooldown;
    }
}
