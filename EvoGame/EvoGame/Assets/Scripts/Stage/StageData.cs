using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Defines an enum StageEventType that enumerates different types of stage events
public enum StageEventType
{
    SpawnEnemy,
    SpawnEnemyBoss,
    SpawnObject,
    WinStage
}


[Serializable]
//Defines a serializable class StageEvent to represent a single stage event.
public class StageEvent
{
    public StageEventType eventType;    //Enum that specifies the type of the event

    public float time;
    public string message;
    public GameObject objectToSpawn;
    public EnemyData enemyToSpawn;

    public int count;                   //Number of times the event should occur.
    public bool isRepeatedEvent;        //Boolean indicating whether the event should be repeated.
    public float repeatEverySeconds;    //Time interval between repeated events.
    public int repeatCount;             //Number of times the event should be repeated.
}

//Defines a ScriptableObject StageData to hold a list of StageEvent instances.
[CreateAssetMenu]
public class StageData : ScriptableObject
{
    public List<StageEvent> stageEvents;

}