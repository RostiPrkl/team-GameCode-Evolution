using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    PlayerStats player;
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }


    protected virtual void Update()
    {
        if (player == null)
            Destroy(gameObject);
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player"))
            Destroy(gameObject);
    }
}
