using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Managment script for background music, 
//ripped from previous project and modified to work in this game.
//forgot I had this.

public class BGMusic : MonoBehaviour
{
    
    private Player player;
    private AudioSource audioSource;
    private bool alreadyPlayed;

    [SerializeField] private AudioClip gameOver;
    [SerializeField] private AudioClip gameStart;
    [SerializeField] private AudioClip gameBG;


      void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player == null)
            Debug.Log("PLAYER NULL");

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            Debug.Log("BGAUDIO NULL");

        StartCoroutine(StartGame());
    }


    void Update()
    {
        if(alreadyPlayed) return;
        //GameOver();
    }

    // void GameOver()
    // {
    //     if (player == null)
    //     {
    //         alreadyPlayed = true;
    //         audioSource.clip = gameOver;
    //         audioSource.Play();
    //     }
    // }

    private IEnumerator StartGame()
    {
        float originalVolume = audioSource.volume;
        audioSource.volume = 1.0f;
        audioSource.PlayOneShot(gameStart);
        yield return new WaitForSeconds(gameStart.length);
        audioSource.clip = gameBG;
        audioSource.Play();
    }
    
}
