using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//responsible for managing stage events in a game.
public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    EnemySpawn enemySpawn;


    StageTime stageTime;
    int eventIndexer;                                                   //An integer variable, used as an index to keep track of the current or upcoming stage event.
    PlayerWinManager playerWin;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();                          //StageTime script is attached to the same GameObject as StageEventManager
    }

    private void Start()
    {
        playerWin = FindObjectOfType<PlayerWinManager>();   
        enemySpawn = FindObjectOfType<EnemySpawn>();    
    }

    //responsible for processing and triggering stage events based on the current stage's time.
    public void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)                //Checks if the eventIndexer exceeds or is equal to the number of stage events in the stageData. 
        {                                                               //If true, it returns early, preventing further execution of the method.
            return;     
        }

        if (stageTime.time > stageData.stageEvents[eventIndexer].time)  //Checks if the current time in the stage has surpassed the time of the current stage event.
        {
            switch (stageData.stageEvents[eventIndexer].eventType)      //Switch statement based on the type of the current stage event. 
            {

                case StageEventType.SpawnEnemy:                         //Calls the SpawnEnemy method with false as an argument, indicating it's not a boss enemy.
                    SpawnEnemy(false);
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;

                case StageEventType.SpawnEnemyBoss:                     //Empty case; no action is taken for spawning a boss enemy in this snippet.
                    break;

            }
                Debug.Log(stageData.stageEvents[eventIndexer].message); //Outputs a debug log with the message associated with the current stage event.
            eventIndexer += 1;                                          //Increments the eventIndexer to move on to the next stage event.



        }
    }

    private void SpawnEnemyBoss()
    {
        SpawnEnemy(true);
    }


    private void WinStage()
    {
        playerWin.Win();
    }

    //responsible for spawning enemies based on the information provided in the current stage event.
    private void SpawnEnemy(bool bossEnemy)
    {
        {
            StageEvent currentEvent = stageData.stageEvents[eventIndexer];  //Retrieves the current stage event from the stageData list based on the current index 
            enemySpawn.AddGroupToSpawn(currentEvent.enemyToSpawn, currentEvent.count, bossEnemy);   //EnemyData representing the type of enemy to spawn, number of enemies to spawn, indicating whether the spawned enemy is a boss enemy.

            if (currentEvent.isRepeatedEvent == true)                       //Checks if the current stage event is a repeated event.
            {
                enemySpawn.AddRepeatedSpawn(currentEvent, bossEnemy);       //If the current event is repeated, calls the AddRepeatedSpawn method on the enemySpawn object.
            }
        }
    }

    //responsible for spawning objects based on the information provided in the current stage event.
    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)                                         //a loop to spawn multiple objects based on the count specified in the current stage event.
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;                                //Sets the initial spawn position to be the position of the player 
            positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));               //generates a random position within a square pattern defined by the given size.

            SpawnManager.instance.SpawnObject(positionToSpawn,stageData.stageEvents[eventIndexer].objectToSpawn);   //The position where the object should be spawned. The GameObject to be spawned, taken from the current stage event.

        }
    }
}
