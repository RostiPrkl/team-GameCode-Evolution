using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //TODO: damage player when hit
    //TODO: take damage from player attack

   [SerializeField] float movementSpeed = 100f;
   [SerializeField] float health = 20f;

   public float enemyDamage;
   private Transform player;
   private Player playerScript; 


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, movementSpeed * Time.deltaTime);
    }


    void OnCollision2dEnter(Collision collision)
    {
        //tätä ei välttis kannata käyttää, testailin vain juttuja
        enemyDamage = 10;
        playerScript.health -= enemyDamage;
    }
}
