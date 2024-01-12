using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames.AudioController;

public class Enemy : MonoBehaviour
{

   [SerializeField] float movementSpeed = 100f;
   [SerializeField] float maxHealth = 200f;
   public float enemyDamage;
   

   float currentHealth;

   Transform player;

    public Gaskellgames.AudioController.SoundController sdcsndmngr;



    void Start()
    {
        currentHealth = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
        if (player == null)
            Destroy(gameObject);
    }

    public void TakeDamage(float dmg)
    {
        sdcsndmngr = GameObject.FindObjectOfType<SoundController>();
        currentHealth -= dmg;
        if (currentHealth <= 0) 
        {
            sdcsndmngr.PlaySoundFX("smallEnemyDie");
            Destroy(gameObject);
        }
            
    }


    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(enemyDamage);
        } 
    }
}
