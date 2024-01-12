using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerScriptableObject playerData;

    [HideInInspector] public float currentHealth;
    float currentRecovery;
    float currentMovementSpeed;
    float currentProjectileSpeed;

    [Header("Leveling stats")]
    public int experience = 0;
    public int level = 0;
    public int expCap;

    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }

    [Header("Invincibility After Damage")]
    [SerializeField] float iFrames;
    float iFrameTimer;
    bool isInvincible;


    public List<LevelRange> levelRanges;
    
    
    void Awake()
    {
        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMovementSpeed = playerData.MovementSpeed;
        currentProjectileSpeed = playerData.ProjectileSpeed;
    }


    void Start()
    {
        //initialization of the exp increase system
        expCap = levelRanges[0].expCapIncrease;
    }


    void Update()
    {
        //reduce iframes and set MORTIS
        if (iFrameTimer > 0)
            iFrameTimer -= Time.deltaTime;
        else if (isInvincible)
            isInvincible = false;
    }


    public void RestoreHealth(float amount)
    {
        currentHealth += amount;
        if (currentHealth > playerData.MaxHealth)
            currentHealth = playerData.MaxHealth;
    }


    public void IncreaseExp(int amount) 
    {
        experience += amount;
        LvlUpChecker();
    }


    void LvlUpChecker()
    {
        if (experience >= expCap)
        {
            level++;
            experience -= expCap;

            int expCapIncrease = 0;
            foreach (LevelRange range in levelRanges)
            {
                if (level >= range.startLevel && level <= range.endLevel)
                {
                    expCapIncrease = range.expCapIncrease;
                    break;
                }
            }
            expCap += expCapIncrease;
        }
    }


    public void TakeDamage(float dmg)
    {
        //Check for iframes, and granting for brief invincibilty after dmg
        if (!isInvincible)
        {
            currentHealth -= dmg;
            iFrameTimer = iFrames;
            isInvincible = true;

            if (currentHealth <= 0)
                Death();
        }
    }


    public void Death()
    {
        Debug.Log("Player has died");
    }
}