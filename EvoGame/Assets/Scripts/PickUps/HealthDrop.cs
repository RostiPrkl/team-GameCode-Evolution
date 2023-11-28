using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDrop : MonoBehaviour, ICollectible
{
    public int healthCollected;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthCollected);

        
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
