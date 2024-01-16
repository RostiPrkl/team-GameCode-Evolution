
using UnityEngine;
using UnityEngine.UI;

public class DebugScreen : MonoBehaviour
{
    [SerializeField] Text debugText;
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject enemySpawn;
    PlayerStats player;
    Enemy_ enemy;
    float updateRate = 0.5f;

    private void Start()
    {
        InvokeRepeating("UpdateDebugInfo", 0f, updateRate);
        enemy = FindObjectOfType<Enemy_>();
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
        debugText.text +=$"Player health: {player.CurrentHealth}\n";

        //Player recovery
        debugText.text +=$"Player regen: {player.CurrentRecovery}\n";

        //Player Base DMG
        debugText.text +=$"Player dmg x: {player.CurrentBaseDamage}\n";

        //Player Pickup
        debugText.text +=$"Player pickrd: {player.CurrentPickupRadius}\n";

        //Player Movement speed
        debugText.text +=$"Player movespd: {player.CurrentMovementSpeed}\n";

        //Player exp
        debugText.text += $"Player xp: {player.experience}\n";

        //Player lvl
        debugText.text += $"Player Level: {player.level}\n";

        //Enemy health
        //debugText.text +=$"Enemy health: {enemy.currentHealth}\n";
    }

    //void Update()
    //{
      // if (Input.GetKeyDown(KeyCode.H))
       // {
         //   SpawnEnemy();
      //  }
   // }


    //void SpawnEnemy()
    //{
        
     //   GameObject newEnemy = Instantiate(enemyPrefab, enemySpawn.transform.position, Quaternion.identity);
        
    //}
}
