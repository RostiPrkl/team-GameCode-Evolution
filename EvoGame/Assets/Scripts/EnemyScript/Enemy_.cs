using System;
using UnityEngine;
using Gaskellgames.AudioController;



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


    public SoundController deathEffect;

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
            deathEffect = FindObjectOfType<SoundController>();


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
        targetPlayer.TakeDamage(stats.damage);

    }

    public void TakeDamage(float currentDamage)
    {
        if (!isDamaged)
        {
            stats.health -= Mathf.FloorToInt(currentDamage);
            isDamaged = true;
        }
        else
            return;

        if (stats.health <= 0)
        {
            switch (enemyData.animatedPrefab.name)
            {
                case "JellyFishEnemy":
                    deathEffect.PlaySoundFX("smallEnemyDie");
                    break;
                case "AnglerEnemy":
                    deathEffect.PlaySoundFX("mediumEnemyDie");
                    break;
                case "RareJelly":
                    deathEffect.PlaySoundFX("mediumEnemyDie");
                    break;
                case "RareAngler":
                    deathEffect.PlaySoundFX("biggerEnemyDie");
                    break;
                case "Boss":
                    deathEffect.PlaySoundFX("bossDie");
                    break;
                default:
                    Debug.Log("NULL STATE");
                    break;
            }

            Destroy(gameObject);
        }
            
    }

}
