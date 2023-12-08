using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[Serializable]
public class  EnemyStats
{
    public int health = 1;
    public int damage = 1;
    public int experience = 200;
    public float moveSpeed = 1f;
    private EnemyStats stats;

    public EnemyStats(EnemyStats stats)
    {              
        this.health = stats.health;
        this.damage = stats.damage;
        this.experience = stats.experience;
        this.moveSpeed = stats.moveSpeed;
    }
}


public class Enemy_ : MonoBehaviour
{
    Transform targetDestination;    
    GameObject targetGameobject;
    Rigidbody2D rb2d;
    PlayerStats targetPlayer;   
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected int coinValue;
    [SerializeField] private GameObject coinObject;
    internal object currentHealth;
    public EnemyStats stats;
    
    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();        
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;

    }

    private void FixedUpdate()
    {
        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb2d.velocity = direction * stats.moveSpeed;
    }


    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject == targetGameobject)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetPlayer == null)
        {
            targetPlayer = targetGameobject.GetComponent<PlayerStats>();
        }
        targetPlayer.TakeDamage(stats.damage);
    }

    private void TakeDamage(int damage)
    {
        stats.health -= damage;

        if (stats.health < 1)
        {
            targetGameobject.GetComponent<PlayerStats>().IncreaseExp(stats.experience);
            var instantiationPoint = sprite.transform;
            var coin = Instantiate(coinObject, instantiationPoint.position, Quaternion.identity);
            coin.gameObject.GetComponent<Coins>().SetValue(coinValue);
            Destroy(gameObject);
        }
    }
}
