using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvoPoint : MonoBehaviour, ICollectible
{
    public int experienceCollected;
    PlayerStats player;


    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }


    void Update()
    {
        if (player == null)
            Destroy(gameObject);
    }


    public void Collect()
    {
        player.IncreaseExp(experienceCollected);
    }


    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
