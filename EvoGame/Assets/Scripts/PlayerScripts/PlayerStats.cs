using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEditor.U2D.Animation;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    PlayerScriptableObject playerData;

    [HideInInspector] public float currentHealth;
    [HideInInspector] public float currentRecovery;
    [HideInInspector] public float currentMovementSpeed;
    [HideInInspector] public float currentProjectileSpeed;
    [HideInInspector] public float currentPickupRadius = 2;

    SpriteRenderer spriteR;

    [SerializeField] AudioClip lowHealthAudio;
    [SerializeField] AudioSource audioSource;

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

    [Header("Invincibility After Damage")]
    [SerializeField] float iFrames;
    float iFrameTimer;
    bool isInvincible;

    [Header("Health Info")]
    [SerializeField] float hpCounter;
    [SerializeField] float hpMaxCounter;
    [SerializeField] Image hpFiller;
    [SerializeField] float previousHealth;

    [Header("Weapons and upgrades")]
    public List<GameObject> spawnedWeapons;

    
    void Awake()
    {
        playerData = CharacterSelector.GetData();
        CharacterSelector.instance.DestroySingleton();

        spriteR = GetComponent<SpriteRenderer>();

        currentHealth = playerData.MaxHealth;
        currentRecovery = playerData.Recovery;
        currentMovementSpeed = playerData.MovementSpeed;
        currentProjectileSpeed = playerData.ProjectileSpeed;
        currentPickupRadius = playerData.PickupRadius;

        SpawnWeapon(playerData.StartingWeapon);
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

        if (currentHealth < 20)
        {
            audioSource.clip = lowHealthAudio;
            audioSource.Play();
        }
        if (currentHealth > 20)
            audioSource.Stop();

        HealthBar();
        XPBar();
        
        Recover();
    }

    public void HealthBar()
    {
        if (hpCounter > hpMaxCounter)
        {
            previousHealth = currentHealth;
            hpCounter = 0;
        }
        else
            hpCounter += Time.deltaTime;

        hpFiller.fillAmount = Mathf.Lerp(previousHealth / playerData.MaxHealth, currentHealth / playerData.MaxHealth, hpCounter / hpMaxCounter);
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
            previousHealth = hpFiller.fillAmount * playerData.MaxHealth;
            hpCounter = 0;
            currentHealth -= dmg;
            StartCoroutine(FlashRed());

            iFrameTimer = iFrames;
            isInvincible = true;

            if (currentHealth <= 0)
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
        if(currentHealth < playerData.MaxHealth)
        {
            currentHealth += currentRecovery * Time.deltaTime;
            if (currentHealth > playerData.MaxHealth)
                currentHealth = playerData.MaxHealth;
        }
    }


    public void Death()
    {
        Debug.Log("Player has died");
    }


    public void SpawnWeapon(GameObject weapon)
    {
        Vector3 spawnPosition = new Vector3(1, 0, 0);
        GameObject spawnedWeapon = Instantiate(weapon, spawnPosition, Quaternion.identity);
        spawnedWeapon.transform.SetParent(transform);
        spawnedWeapons.Add(spawnedWeapon);
    }
}
