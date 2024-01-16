using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    #region Player inspector
    [Header("Leveling stats")]
    public int experience = 0;
    [SerializeField] float previousexperience;
    [SerializeField] Image xpFiller;
    [SerializeField] float xpCounter;
    [SerializeField] float xpMaxCounter;
    public int level = 0;
    int previousLevel;
    public int expCap;
    public List<LevelRange> levelRanges;
    
    [System.Serializable]
    public class LevelRange
    {
        public int startLevel;
        public int endLevel;
        public int expCapIncrease;
    }

    [Header("Inventory")]
    public int AttackIndex;
    public int PassiveIndex;
    InventoryManager inventory;

    [Header("Health Info")]
    [SerializeField] float hpCounter;
    [SerializeField] float hpMaxCounter;
    [SerializeField] Image hpFiller;
    [SerializeField] float previousHealth;

    [Header("Invincibility After Damage")]
    [SerializeField] float iFrames;
    float iFrameTimer;
    bool isInvincible;

    [Header("Player Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip lowHealthAudio, deathAudio, dashAudio;
    #endregion

    #region Current player stats
    float currentHealth;
    float currentRecovery;
    float currentMovementSpeed;
    float currentBaseDamage;
    float currentPickupRadius = 2f;
    //float currentProjectileSpeed;
    public float CurrentHealth
    {
        get { return currentHealth; }
        set
        {
            if (currentHealth != value)
                currentHealth = value;
        }
    }
    public float CurrentRecovery
    {
        get { return currentRecovery; }
        set
        {
            if (currentRecovery != value)
                currentRecovery = value;
        }
    }
    public float CurrentMovementSpeed
    {
        get { return currentMovementSpeed; }
        set
        {
            if (currentMovementSpeed != value)
                currentMovementSpeed = value;
        }
    }
    // public float CurrentProjectileSpeed
    // {
    //     get { return currentProjectileSpeed; }
    //     set
    //     {
    //         if (currentProjectileSpeed != value)
    //             currentProjectileSpeed = value;
    //     }
    // }
    public float CurrentBaseDamage
    {
        get { return currentBaseDamage; }
        set
        {
            if (currentBaseDamage != value)
                currentBaseDamage = value;
        }
    }
    public float CurrentPickupRadius
    {
        get { return currentPickupRadius; }
        set
        {
            if (currentPickupRadius != value)
                currentPickupRadius = value;
        }
    }
    
    #endregion
   
    #region Misc
    PlayerScriptableObject playerData;
    SpriteRenderer spriteR;
    #endregion

    
    void Awake()
    {
        spriteR = GetComponent<SpriteRenderer>();
        inventory = GetComponent<InventoryManager>();

        playerData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        CurrentHealth = playerData.MaxHealth;
        CurrentRecovery = playerData.Recovery;
        CurrentMovementSpeed = playerData.MovementSpeed;
        //CurrentProjectileSpeed = playerData.ProjectileSpeed;
        CurrentBaseDamage = playerData.BaseDamage;
        CurrentPickupRadius = playerData.PickupRadius;

        SpawnAttack(playerData.StartingAttack);
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

        HealthBar();
        XPBar();
        
        Recover();
    }

    public void HealthBar()
    {
        if (hpCounter > hpMaxCounter)
        {
            previousHealth = CurrentHealth;
            hpCounter = 0;
        }
        else
            hpCounter += Time.deltaTime;

        hpFiller.fillAmount = Mathf.Lerp(previousHealth / playerData.MaxHealth, CurrentHealth / playerData.MaxHealth, hpCounter / hpMaxCounter);
    }


    public void XPBar()
    {

        LvlUpChecker();

        if (experience == 0)
        {
            xpFiller.fillAmount = 0;
            previousexperience = 0;
        }

        if (experience > previousexperience)
        {
            previousexperience = experience;
            xpCounter = 0;
            xpCounter += Time.deltaTime;
            xpFiller.fillAmount = Mathf.Lerp(previousexperience / expCap, experience / expCap, xpCounter / xpMaxCounter);
        }        
    }


    public void RestoreHealth(float amount)
    {
        CurrentHealth += amount;
        if (CurrentHealth > playerData.MaxHealth)
            CurrentHealth = playerData.MaxHealth;
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

            GameManager.instance.StartEvolution();
        }
    }


    public void TakeDamage(float dmg)
    {
        //Check for iframes, and granting for brief invincibilty after dmg
        if (!isInvincible)
        {
            previousHealth = hpFiller.fillAmount * playerData.MaxHealth;
            hpCounter = 0;
            CurrentHealth -= dmg;

            if (CurrentHealth < 20)
            {
                audioSource.clip = lowHealthAudio;
                audioSource.Play();
            }
            else
                audioSource.Stop();

            StartCoroutine(FlashRed());

            iFrameTimer = iFrames;
            isInvincible = true;

            if (CurrentHealth <= 0)
                Death();
        }
    }


    private IEnumerator FlashRed()
    {
        Color originalColor = spriteR.color;
        spriteR.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteR.color = originalColor;
    }


    void Recover()
    {
        if(CurrentHealth < playerData.MaxHealth)
        {
            CurrentHealth += CurrentRecovery * Time.deltaTime;
            if (CurrentHealth > playerData.MaxHealth)
                CurrentHealth = playerData.MaxHealth;
        }
    }


    public void Death()
    {
        if (!GameManager.instance.isGameOver)
        {
            Debug.Log("Player has died");
            GameManager.instance.GameOver();
        }
    }


    public void SpawnAttack(GameObject attack)
    {
        if (AttackIndex >= inventory.attackSlots.Count -1)
        {
            Debug.LogError("INVENTORY FULL");
            return;
        }

        //leetle hack to get the spwans in the right place

            GameObject spawnedAttack = Instantiate(attack, transform.position, Quaternion.identity);
            spawnedAttack.transform.SetParent(transform);
            inventory.AddAttack(AttackIndex, spawnedAttack.GetComponent<PlayerAttackController>());
            AttackIndex++;
    }


    public void SpawnPassive(GameObject passive)
    {
        if (PassiveIndex >= inventory.passiveSlots.Count -1)
        {
            Debug.LogError("INVENTORY FULL");
            return;
        }

        GameObject spawnedPassive = Instantiate(passive, transform.position, Quaternion.identity);
        spawnedPassive.transform.SetParent(transform);
        inventory.AddPassive(PassiveIndex, spawnedPassive.GetComponent<PassiveItem>());
        PassiveIndex++; //each attack is it's own slot. no overlap
    }
}