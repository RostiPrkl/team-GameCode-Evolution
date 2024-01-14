using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;


public class Enemy_ : MonoBehaviour

{
    public EnemyData enemyData;
    Transform targetDestination;   
    GameObject targetGameobject;     
    PlayerStats targetPlayer;
    //[SerializeField] protected SpriteRenderer sprite;
    //[SerializeField] protected int coinValue;
    //[SerializeField] private GameObject coinObject;

    [SerializeField] float currentHealth;
    [SerializeField] float currentMovespeed;
    [SerializeField] float currentDamage;
    
    Rigidbody2D rb2d;
    internal object stats;

    private void Awake()

    {

        rb2d = GetComponent<Rigidbody2D>();
        //enemyData = FindObjectOfType<EnemyData>();

    }

    private void Start()
    {
        if (enemyData != null) 
        {
           
            //stats = GetComponent<Enemy_>
            SetTarget(GameManager.instance.playerTransform.gameObject);
            currentHealth = enemyData.Health;
            currentMovespeed = enemyData.MoveSpeed;
            currentDamage = enemyData.Damage;
            

        }
    }

    public void SetTarget(GameObject target)
    {
        targetGameobject = target;
        targetDestination = target.transform;

    }

    // internal void UpdateStatsForProgress(float progress)
    // {
    //     stats.ApplyProgress(progress);
    // }
    
    private void FixedUpdate()

    {

        Vector3 direction = (targetDestination.position - transform.position).normalized;
        rb2d.velocity = direction * currentMovespeed;

    }

    // internal void SetStats(EnemyStats stats)
    // {
    //     this.stats = new EnemyStats(stats);
    // }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerStats player = other.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }
        
    }

    public void TakeDamage(float dmg)
    {
        currentHealth -= dmg;
        if (currentHealth <= 0f)
            Death();
    }


    public void Death()
    {

        Destroy(gameObject);
    }


    private void OnDestroy()
    {
        EnemySpawn es = FindObjectOfType<EnemySpawn>();
        es.OnEnemyKIlled();
    }

}
