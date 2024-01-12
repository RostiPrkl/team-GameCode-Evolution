using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames.AudioController;

public class Pickup : MonoBehaviour, ICollectible
{
    public bool isCollected = false;
    PlayerStats player;
    public Gaskellgames.AudioController.SoundController sdcsndmngr;
    protected virtual void Start()
    {
        player = FindObjectOfType<PlayerStats>();
    }


    protected virtual void Update()
    {
        if (player == null)
            Destroy(gameObject);
    }


    public virtual void Collect()
    {
        isCollected = true;
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag("Player") && isCollected)
        {
            Destroy(gameObject);
            isCollected = true;
            sdcsndmngr = GameObject.FindObjectOfType<SoundController>();
            sdcsndmngr.PlaySoundFX("experience1");
        }
    }
}
