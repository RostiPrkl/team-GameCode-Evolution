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
}
