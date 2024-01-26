using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{

    private int _coinValue = 1;
    private bool collected;

    public void SetValue(int value)
    {
        _coinValue = value;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (collected) return;
        if (!other.CompareTag("PlayerController")) return;
        var player = other.gameObject.GetComponent<PlayerStats>();
        if (player == null) return;
        player.IncreaseExp(_coinValue);
        collected = true;
        Destroy(gameObject);
    }
}

