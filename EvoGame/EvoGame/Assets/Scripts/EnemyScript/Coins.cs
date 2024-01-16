using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    private int _coinValue = 1; 
    private bool collected; //A boolean flag to track whether the coin has already been collected to prevent multiple collections.

    //initializing the coin with a specific value.
    public void SetValue(int value)
    {
        _coinValue = value;
    }

    //called when a Collider2D enters the trigger zone of the coin.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;                                      //Checks if the coin has already been collected.
        if (!other.CompareTag("PlayerController")) return;          //Checks if the collider entering the trigger zone has the tag "PlayerController". If not, the method is skipped.
        var player = other.gameObject.GetComponent<PlayerStats>();  //Retrieves the PlayerStats component from the GameObject associated with the collider.
        if (player == null) return;                                 //Checks if the PlayerStats component was successfully retrieved.
        player.IncreaseExp(_coinValue);                             //Calls the IncreaseExp method on the player's PlayerStats component, passing the coin's value as an argument. 
        collected = true;                                           //Sets the collected flag to true to prevent multiple collections.
        Destroy(gameObject);                                        //Destroys the coin GameObject, removing it from the game after it has been collected.
    }
}

