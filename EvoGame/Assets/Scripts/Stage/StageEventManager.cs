using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StageEventManager : MonoBehaviour
{
    [SerializeField] StageData stageData;
    EnemySpawn enemySpawn;

    StageTime stageTime;
    int eventIndexer;
    [SerializeField] GameObject winMessagePanel;

    private void Awake()
    {
        stageTime = GetComponent<StageTime>();
    }

    private void Start()
    {
        //playerWin = FindObjectOfType<PlayerWinManager>();
        enemySpawn = FindObjectOfType<EnemySpawn>();
        winMessagePanel.SetActive(false);
    }

    public void Update()
    {
        if (eventIndexer >= stageData.stageEvents.Count)
        {
            return;
            
        }

        if (stageTime.time > stageData.stageEvents[eventIndexer].time)
        {
            switch (stageData.stageEvents[eventIndexer].eventType)
            {

                case StageEventType.SpawnEnemy:
                    SpawnEnemy(false);
                    break;

                case StageEventType.SpawnObject:
                    SpawnObject();
                    break;

                case StageEventType.WinStage:
                    WinStage();
                    break;

                case StageEventType.SpawnEnemyBoss:
                    SpawnEnemyBoss();
                    break;

            }
                Debug.Log(stageData.stageEvents[eventIndexer].message);
                eventIndexer += 1;


            
        }

    }

    private void SpawnEnemyBoss()
    {
        SpawnEnemy(true);
    }


    public void WinStage()
    {
        Time.timeScale = 0f;
        Debug.Log("YOU WON");
        winMessagePanel.SetActive(true);
    }

    private void SpawnEnemy(bool bossEnemy)
    {
        {
            StageEvent currentEvent = stageData.stageEvents[eventIndexer];
            enemySpawn.AddGroupToSpawn(currentEvent.enemyToSpawn, currentEvent.count, bossEnemy);

            if (currentEvent.isRepeatedEvent == true)
            {
                enemySpawn.AddRepeatedSpawn(currentEvent, bossEnemy);
            }
        }
    }

    private void SpawnObject()
    {
        for (int i = 0; i < stageData.stageEvents[eventIndexer].count; i++)
        {
            Vector3 positionToSpawn = GameManager.instance.playerTransform.position;
            positionToSpawn += UtilityTools.GenerateRandomPositionSquarePattern(new Vector2(5f, 5f));

            SpawnManager.instance.SpawnObject(positionToSpawn,stageData.stageEvents[eventIndexer].objectToSpawn);
        }
    }
}
