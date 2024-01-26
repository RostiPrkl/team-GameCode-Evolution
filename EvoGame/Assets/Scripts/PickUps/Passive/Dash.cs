using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dashTime = 0.5f;
    public float coolDown = 5f;
    private bool isDashing = false;
    Player player;
    float regularSpeed;


    void Start()
    {
        player = FindObjectOfType<Player>();
        player.GetComponent<Player>().dashEvolution = GetComponent<Dash>();
        regularSpeed = player.playerStats.CurrentMovementSpeed;
    }


    public void ActivateDash()
    {
        if (!isDashing)
        {
            StartCoroutine(DashCoroutine());  
        }
    }

    IEnumerator DashCoroutine()
    {
        isDashing = true;
        player.playerStats.CurrentMovementSpeed *= 5;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        player.playerStats.CurrentMovementSpeed = regularSpeed;
        yield return new WaitForSeconds(coolDown);
    }
}
