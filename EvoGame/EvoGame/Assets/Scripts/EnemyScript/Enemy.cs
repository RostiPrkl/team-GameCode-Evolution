using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class EnemyStats
{
    //Enemy Stats 
    public float health = 1;
    public int damage = 1;
    public int experience = 200;
    public float moveSpeed = 1f;
    

    //create a new instance of EnemyStats by copying the values from an existing EnemyStats object. 
    public EnemyStats(EnemyStats stats)
    {
        this.health = stats.health;
        this.damage = stats.damage;
        this.experience = stats.experience;
        this.moveSpeed = stats.moveSpeed;
        
    }

    //This method takes one parameter, progress, which is a float representing the progress value.
    internal void ApplyProgress(float progress)
    {
        this.health = (int)(health * progress);
        this.damage = (int)(damage * progress);
        
    }
}
public class Enemy : MonoBehaviour, IDamageable

{
    
                   
    [SerializeField] protected SpriteRenderer sprite;
    [SerializeField] protected int coinValue;
    [SerializeField] private GameObject coinObject;
    [SerializeField] EnemyData enemyData;
    
    Rigidbody2D rb2d;
    PlayerStats targetPlayer;
    Transform targetDestination;
    

    public EnemyStats stats;
    GameObject targetGameobject;



    private void Awake()

    {

        rb2d = GetComponent<Rigidbody2D>();                                                         //allows the script to interact with the Rigidbody2D component

    }

    //checking if the enemyData field is not null, and if it's not, it proceeds to set the stats and target for the enemy.
    private void Start()
    {
        if (enemyData != null)                                                                      //checks if the enemyData field is not null
        {
            InitSprite(enemyData.animatedPrefab);
            SetStats(enemyData.stats);                                                              //sets the stats of the enemy based on the provided EnemyStats object.
            SetStats(new EnemyStats(enemyData.stats));
            SetTarget(GameManager.instance.playerTransform.gameObject);                             //setting the target for the enemy to be the player GameObject. It retrieves the player's transform from the GameManager instance.

        }
    }

    //takes a GameObject as a parameter and sets the enemy's target properties based on that GameObject
    public void SetTarget(GameObject target)                                                        // takes a GameObject parameter named target
    {
                                                                                                    //targetGameobject = target;//represents the GameObject that the enemy is targeting.
        if (target == null) return;
        targetPlayer = target.GetComponent<PlayerStats>(); 
        targetDestination = target.transform;                                                       //represents the destination the enemy is moving towards.

    }

    //responsible for updating the stats of the enemy based on a progress parameter.
    internal void UpdateStatsForProgress(float progress)
    {
        stats.ApplyProgress(progress);                                                              //Calls the ApplyProgress method on the stats object. It passes the progress parameter to this method.
    }

    //responsible for setting the velocity of the enemy based on its target destination.
    private void FixedUpdate()

    {
        if (targetDestination != null)
        {
            Vector3 direction = (targetDestination.position - transform.position).normalized;       //Calculates the direction from the current position of the enemy to the target destination.
            rb2d.velocity = direction * stats.moveSpeed;                                            //Sets the velocity of the enemy's, based on the calculated direction and the move speed from the stats object.
        }
    }

    //set the stats of the enemy based on an input EnemyStats object
    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);                                                         //Creates a new instance of EnemyStats by using the stats parameter as a blueprint.
                                                                                                    //To avoid direct references and to ensure that changes to the original stat object outside of the Enemy class do not affect this instance's stats.
    }

    //this detect and respond to collisions with another GameObject.
    private void OnCollisionStay2D(Collision2D collision)

    {
        //if (collision.gameObject == targetGameobject)                                             //Checks if the GameObject involved in the collision is the same as the target GameObject that the enemy is currently targeting.
        if (collision.gameObject == targetPlayer.gameObject)
                                                                                                    //If the collision involves the target GameObject, it calls the Attack method.
        {
            Attack();                                                                               //this method is responsible for initiating an attack when the enemy is in contact with its target.
        }
    }

    //responsible for initiating an attack
    private void Attack()

    {
        if (targetPlayer == null)                                                                 //Checks if the targetPlayer variable is null.To ensure that the targetPlayer is properly assigned before attempting to perform actions on it.
                                                                                                  //if (targetPlayer != null)
        {
            targetPlayer = targetGameobject.GetComponent<PlayerStats>();                          //If targetPlayer is null, it gets the PlayerStats component from the target GameObject and assigns it to the targetPlayer variable.
                                                                                                  //targetPlayer.TakeDamage(stats.damage);
        }
        targetPlayer.TakeDamage(stats.damage);                                                    //Calls the TakeDamage method on the PlayerStats component of the target player. Passes the damage value from the stats object as an argument.

    }                                                                                             //The Attack method assumes that the target GameObject has a PlayerStats component, and it may need additional error handling or validation depending on the specific requirements of the game.


    //This method is called when the player takes damage.
    public void TakeDamage(float currentDamage)
    {
        stats.health -= currentDamage;                                                              //Reduces the health of the enemy by the amount of damage (currentDamage) received. 

        if (stats.health < 1)                                                                       // Checks if the enemy's health is less than 1 after taking damage. If true, the enemy is considered defeated.
        {
                                                                                                    //targetPlayer.IncreaseExp(stats.experience);
            targetPlayer.GetComponent<PlayerStats>().IncreaseExp(stats.experience);                 // Retrieves the PlayerStats component from the target GameObject and calls its IncreaseExp method, passing the experience value from the stats object.
            var instantiationPoint = sprite.transform;                                              //Retrieves the transform of the enemy's sprite. This as the position where a coin is instantiated.
            var coin = Instantiate(coinObject, instantiationPoint.position, Quaternion.identity);   //Instantiates a coin GameObject at the position of the enemy's sprite. The coin object is instantiated based on the coinObject template.
            coin.GetComponent<Coins>().SetValue(coinValue);                                         //Gets the Coins component from the instantiated coin object and sets its value using the SetValue method, passing the coinValue.
            
            Destroy(gameObject);                                                                    //Destroys the enemy GameObject. This is done after the enemy has been defeated, and its health has dropped below 1.
        } 
    }

    internal void InitSprite(GameObject animatedPrefab)
    {
        GameObject spriteObject = Instantiate(animatedPrefab);
        spriteObject.transform.parent = transform;
        spriteObject.transform.localPosition = Vector3.zero;
    }

}
