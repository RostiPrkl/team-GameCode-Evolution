using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoPoint : MonoBehaviour, ICollectible
{
    public int experienceCollected;

    public void Collect()
    {
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.IncreaseExp(experienceCollected);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
