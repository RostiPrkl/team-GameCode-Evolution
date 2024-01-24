using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceBackRecovery : MonoBehaviour
{
    public float healPercentageThreshold = 0.1f;
    public float cooldownDuration = 30f;

    [SerializeField] bool isOnCooldown = false;
    [SerializeField] float lastActivationTime;

    public PlayerStats player;


    private void Awake()
    {
        player = FindObjectOfType<PlayerStats>();
        //lastActivationTime = -cooldownDuration;
    }

    private void Update()
    {
        
        if (!isOnCooldown && player.CurrentHealth < 10f)
        {
            player.CurrentHealth = player.newMaxHealth;
            isOnCooldown = true;
        }
        else if (isOnCooldown)
        {
            lastActivationTime += Time.deltaTime;

            if (lastActivationTime >= cooldownDuration)
            {
                isOnCooldown = false;
                lastActivationTime = 0;
            }
        }
    }
}
