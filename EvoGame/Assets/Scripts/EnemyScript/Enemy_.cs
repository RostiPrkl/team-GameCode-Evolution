using System;
using UnityEngine;




[Serializable]
public class EnemyStats
{
    // Vihollisen tilastot. 
    public float health = 1;
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
    //SerializeField] protected SpriteRenderer sprite;
    //[SerializeField] protected int coinValue;
    //[SerializeField] private GameObject coinObject;
    [SerializeField] EnemyData enemyData;
    internal object currentHealth;
    Rigidbody2D rb2d;
    public bool isDamaged = false;


    public AudioManager enemySound;

    private void Awake()

    {

        rb2d = GetComponent<Rigidbody2D>();
        targetPlayer = FindObjectOfType<PlayerStats>();
    }

    private void Start()
    {
        if (enemyData != null) 
        {
           
            SetStats(enemyData.stats);
            SetTarget(GameManager.instance.playerTransform.gameObject);
            enemySound = FindObjectOfType<AudioManager>();


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
        isDamaged = false;
    }

    internal void SetStats(EnemyStats stats)
    {
        this.stats = new EnemyStats(stats);
    }

    private void OnTriggerStay2D(Collider2D collision)

    {
        if (collision.gameObject.CompareTag("Player"))

        {
            Attack();
        }
    }

    private void Attack()

    {
        switch (enemyData.animatedPrefab.name)
        {
            case "JellyFishEnemy":
                {
                    if (enemySound.IsSoundPlaying(30) == false)
                    {
                        enemySound.PlayDelayedEffect(2.0f,30);
                    }
                    else
                        enemySound.PlayEffect(30);  
                }
                break;
            case "AnglerEnemy":
                {
                    if (enemySound.IsSoundPlaying(31) == false)
                    {
                        enemySound.PlayDelayedEffect(0.3f,31);
                    }
                    else
                        enemySound.PlayEffect(31);
                }
                break;
            case "JellyFishEnemyRare":
                {
                    if (enemySound.IsSoundPlaying(30) == false)
                    {
                        enemySound.PlayDelayedEffect(2.0f,30);
                    }
                    else
                        enemySound.PlayEffect(30);
                }
                break;
            case "AnglerEnemyRare":
                {
                    if (enemySound.IsSoundPlaying(31) == false)
                    {
                        enemySound.PlayDelayedEffect(0.3f,31);
                    }
                    else
                        enemySound.PlayEffect(31);
                }
                break;
            case "Boss":
                {
                    if (enemySound.IsSoundPlaying(31) == false)
                    {
                        enemySound.PlayEffect(31);
                    }
                }
                break;
            default:
                Debug.Log("NULL STATE");
                break;
        }
        targetPlayer.TakeDamage(stats.damage);

    }

    public void TakeDamage(float currentDamage)
    {

        stats.health -= Mathf.FloorToInt(currentDamage);

        if (stats.health <= 0)
        {
            Die();
        }
            
    }

    void Die()
    {
        switch (enemyData.animatedPrefab.name)
            {
                case "JellyFishEnemy":
                    {
                        if (enemySound.IsSoundPlaying(24) == false)
                        {
                            enemySound.PlayEffect(24);
                        }
                    }
                    break;
                case "AnglerEnemy":
                    {
                        if (enemySound.IsSoundPlaying(17) == false)
                        {
                            enemySound.PlayEffect(17);
                        }
                    }
                    break;
                case "JellyFishEnemyRare":
                    {
                        if (enemySound.IsSoundPlaying(24) == false)
                        {
                            enemySound.PlayEffect(24);
                        }
                    }
                    break;
                case "AnglerEnemyRare":
                    {
                        if (enemySound.IsSoundPlaying(0) == false)
                        {
                            enemySound.PlayEffect(0);
                        }
                    }
                    break;
                case "TestEnemy":
                    {
                        if (enemySound.IsSoundPlaying(1) == false)
                        {
                            enemySound.PlayEffect(1);
                        }
                        FindObjectOfType<StageEventManager>().WinStage();
                    }
                    break;
                default:
                    Debug.Log("NULL STATE");
                    break;
            }

            Destroy(gameObject);
    }

}
