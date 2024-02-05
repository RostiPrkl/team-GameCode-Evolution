using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class EnemyStats
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

    internal void ApplyProgress(float progress)
    {
        this.health = (int)(health * progress);
        this.damage = (int)(damage * progress);
    }
}
public class Enemy_ : MonoBehaviour

{
    public EnemyStats stats;
    Transform targetDestination;   
    GameObject targetGameobject;     
    PlayerStats targetPlayer;
    //[SerializeField] protected SpriteRenderer sprite;
    //[SerializeField] protected int coinValue;
    //[SerializeField] private GameObject coinObject;
    [SerializeField] EnemyData enemyData;
    internal object currentHealth;
    
    Rigidbody2D rb2d;

    private void Awake()

    {

        rb2d = GetComponent<Rigidbody2D>();

    }

    private void Start()
    {
        if (enemyData != null) 
        {
           
            SetStats(enemyData.stats);
            SetTarget(GameManager.instance.playerTransform.gameObject);
            

        }
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;

    }

    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);
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


    public void TakeDamage(int currentDamage)
    {
        stats.health -= currentDamage;
        

        if (stats.health < 1)
        {

            targetGameobject.GetComponent<PlayerStats>().IncreaseExp(stats.experience);
            //var instantiationPoint = sprite.transform;
            //var coin = Instantiate(coinObject, instantiationPoint.position, Quaternion.identity);
            //coin.gameObject.GetComponent<Coins>().SetValue(coinValue);
            Destroy(gameObject);
        }
    }

    

}
