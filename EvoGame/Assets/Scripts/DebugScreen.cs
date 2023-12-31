using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    [SerializeField] Text debugText;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemySpawn;
    PlayerStats player;
    float updateRate = 0.5f;

    private void Start()
    {
        InvokeRepeating("UpdateDebugInfo", 0f, updateRate);
        player = FindObjectOfType<PlayerStats>();
    }

    private void UpdateDebugInfo()
    {
        if (debugText == null)
        {
            Debug.LogWarning("DebugText not assigned!");
            return;
        }

        // Display FPS
        float fps = 1f / Time.deltaTime;
        debugText.text = $"FPS: {fps:F1}\n";

        //Player Health
        debugText.text +=$"Player health: {player.currentHealth}\n";

        //Player exp
        debugText.text += $"Player xp: {player.experience}\n";

        //Player lvl
        debugText.text += $"Player Level: {player.level}\n";
    }
}
