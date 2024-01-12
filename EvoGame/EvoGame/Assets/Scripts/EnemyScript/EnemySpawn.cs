using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;     //Represents the data or configuration for the type of enemy to be spawned.
    public int count;               //Represents the number of enemies in the group that should be spawned.
    public bool isBoss;             //A boolean indicating whether the group represents a boss enemy.

    public float repeatTimer;       //Represents the timer for repeated spawns. It's a countdown timer that determines when the next spawn should occur.
    public float timeBetweenSpawn;  //Represents the time interval between repeated spawns. specifies how much time should elapse before the next spawn occurs after the timer reaches 0.
    public int repeatCount;         //Represents the number of times the group should be repeatedly spawned. It decrements each time a spawn occurs, and when it reaches 0, the group is considered complete.



    public EnemiesSpawnGroup(EnemyData enemyData, int count, bool isBoss)
    {
        this.enemyData = enemyData;
        this.count = count;
        this.isBoss = isBoss;
    }
    public void SetRepeatSpawn(float timeBetweenSpawn, int repeatCount)
    {
        this.timeBetweenSpawn = timeBetweenSpawn;
        this.repeatCount = repeatCount;
        repeatTimer = timeBetweenSpawn; //Initializes the repeatTimer property with the value of timeBetweenSpawn. Used as a countdown timer to determine when the next spawn should occur during repeated spawns.

    }
}
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;
    //[SerializeField] float spawnTimer;

    List<Enemy> bossEnemiesList;
    float totalBossHealth;
    float currentBossHealth;
    [SerializeField] Slider bossHealthBar;
    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    int spawnPerFrame = 2;

    private void Start()
    {
        player = GameManager.instance.playerTransform.gameObject;                     //Retrieves the player's GameObject from the GameManager instance.
        bossHealthBar = FindObjectOfType<BossHealthBar>(true).GetComponent<Slider>(); //Finding and Assigning Boss Health Bar: retrieves the Slider component from the found BossHealthBar GameObject and assigns it to the bossHealthBar variable.
        stageProgress = FindObjectOfType<StageProgress>();                            //Finding and Assigning Stage Progress: assigns the found StageProgress component to the stageProgress variable.
    }

    //This Update method is orchestrates the main functionality of the spawning system and boss health updating.
    private void Update()       
    {
        
        UpdateRepeatedSpawnGroup();
        ProcessSpawn();
        UpdateBossHealth();
    }


    //handle the repeated spawning of enemy groups based on a timer.           
    private void UpdateRepeatedSpawnGroup() 
    {
        if (repeatedSpawnGroupList == null) { return; }                                                                                     //Null Check
        for (int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)                                                                         //Loop Through Repeated Spawn Groups
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;                                                                        //Timer Update:Decrements the repeatTimer of the current repeated spawn group by the time passed since the last frame
            if (repeatedSpawnGroupList[i].repeatTimer < 0)                                                                                  //Check Timer Expiry: Checks if the repeatTimer of the current repeated spawn group is less than 0, indicating that it's time to spawn another group.
            {   //Spawn Logic:
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;                                         //If the timer has expired, it resets the repeatTimer to the timeBetweenSpawn value.
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);    //Calls the AddGroupToSpawn method to add a new group to the spawn list with the same parameters as the repeated spawn group.
                repeatedSpawnGroupList[i].repeatCount -= 1;                                                                                 //Decrements the repeatCount of the current repeated spawn group by 1.
                if (repeatedSpawnGroupList[i].repeatCount <= 0)                                                                             //Checks if the repeatCount is now less than or equal to 0
                {
                    repeatedSpawnGroupList.RemoveAt(i);                                                                                     //removes the repeated spawn group from the list, indicating that all repetitions have been completed.
                }
            }
        }
    }

    //defines a method called ProcessSpawn that is responsible for spawning enemies
    private void ProcessSpawn()
    {
        if (enemiesSpawnGroupList == null) { return; }                                           //Null Check
        for (int i = 0; i < spawnPerFrame; i++)                                                  //Spawn Loop:represents the number of enemies to spawn per frame.
        {    //Spawn Logic
            if (enemiesSpawnGroupList.Count > 0)                                                 //Checks if there are groups of enemies in the enemiesSpawnGroupList.
            {
                if (enemiesSpawnGroupList[0].count <= 0) { return; }                             //If true, checks if the number of the first group is less than or equal to 0. If true, returns the method indicating that the group has been used up.
                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss); //Calls the SpawnEnemy method with the enemy data and a boolean indicating whether the group is a boss group.
                enemiesSpawnGroupList[0].count -= 1;                                             //Decrements the count of the first group by 1
                if (enemiesSpawnGroupList[0].count <= 0)                                         //Checks if the count of the first group is now less than or equal to 0:
                {
                    enemiesSpawnGroupList.RemoveAt(0);                                           //If true, removes the first group from the list, assuming all enemies in that group have been spawned.
                }

            }
        }
    }


    private void UpdateBossHealth()
    {
        if (bossEnemiesList == null) { return; }                  //Null Check
        if (bossEnemiesList.Count == 0) { return; }               //Empty List Check

        currentBossHealth = 0;                                    //Resetting Current Boss Health

        for (int i = 0; i < bossEnemiesList.Count; i++)           //Loop Through Boss Enemies List
        {
            if (bossEnemiesList[i] == null) { continue; }         //Null Check:If the current element at index i is null, the loop continues to the next iteration
            currentBossHealth += bossEnemiesList[i].stats.health; //Updating Current Boss Health:
        }
        bossHealthBar.value = currentBossHealth;                  //Updating Boss Health Bar

        if (currentBossHealth < 0)                                //Checking for Boss Defeat
        {
            bossHealthBar.gameObject.SetActive(false);            //Disabling Boss Health Bar
            bossEnemiesList.Clear();                              //Clearing Boss Enemies List
        }
    }


    //managing groups of enemies to be spawned
    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)                       //Parameters:type of enemy to spawn,number of enemies in the group to spawn and A boolean indicating whether the group being added is a boss group.
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);       //Creating a New EnemiesSpawnGroup: Instantiates a new EnemiesSpawnGroup object using the provided parameters. 

        if (enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); } //Null Check and List Initialization: Checks if enemiesSpawnGroupList is null. If it is, it initializes it as a new empty list
        enemiesSpawnGroupList.Add(newGroupToSpawn);                                                   //Adding the Group to the List:
    }

    //responsible for spawning a regular enemy in the game
    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)

    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea); //generate a random position within a square pattern defined by spawnArea.
        position += player.transform.position;                                          //Offsets the generated position by the player's current position.

        GameObject newEnemy = Instantiate(enemy);                                       //Instantiates a new enemy GameObject based on the provided enemy prefab (enemy).
        newEnemy.transform.position = position;                                         //Sets the position of the new enemy GameObject to the previously generated position.
        Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();                       //Retrieves the Enemy component from the newly instantiated enemy GameObject.
        newEnemyComponent.SetTarget(player);                                            //Sets the target for the new enemy to be the player.
        newEnemyComponent.SetStats(enemyToSpawn.stats);                                 //Sets the stats of the new enemy based on the provided EnemyData object.
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);               //Updates the stats of the new enemy based on the current progress of the stage.
        //newEnemy.transform.parent = transform;                                          //Sets the EnemySpawn GameObject as the parent of the new enemy.

        //spawning sprite object of the enemy
        newEnemyComponent.InitSprite(enemyToSpawn.animatedPrefab);
        

        if (isBoss == true)                                                             //Checks if the spawned enemy is a boss
        {
            SpawnBossEnemy(newEnemyComponent);                                          //and if true, it calls a method (SpawnBossEnemy) to handle additional logic specific to boss enemies.
        }
    }


    // spawning and managing boss enemies
    private void SpawnBossEnemy(Enemy newBoss)
    {
        if (bossEnemiesList == null) { bossEnemiesList = new List<Enemy>(); } //Null Check and List Initialization:

        bossEnemiesList.Add(newBoss);                                         //Add Boss to List:

        totalBossHealth += newBoss.stats.health;                              //Adds the health of the newly spawned boss to the total boss health. 
        bossHealthBar.gameObject.SetActive(true);                             //Activates the boss health bar.
        bossHealthBar.maxValue = totalBossHealth;                             //Sets the maximum value of the boss health bar to the total boss health.
    }

    //responsible for adding a group of enemies with repeated spawns (defined by a StageEvent) to the list of repeated spawns. 
    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss); //Create Repeated Spawn Group:
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);                        //Calls the SetRepeatSpawn method of the repeatSpawnGroup to set up the repeated spawning parameters based on the StageEvent information.
        if (repeatedSpawnGroupList == null)                                                                            //Null Check  
        {
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();                                                    //List Initialization:
        }

        repeatedSpawnGroupList.Add(repeatSpawnGroup);                                                                  //Add Repeated Spawn Group to List:
    }
}
