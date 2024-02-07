using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemiesSpawnGroup
{
    public EnemyData enemyData;
    public int count;
    public bool isBoss;

    public float repeatTimer;
    public float timeBetweenSpawn;
    public int repeatCount;



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
        repeatTimer = timeBetweenSpawn;

    }
}
public class EnemySpawn : MonoBehaviour
{
    [SerializeField] StageProgress stageProgress;
    //[SerializeField] GameObject enemy;    
    [SerializeField] Vector2 spawnArea;
    [SerializeField] GameObject player;

    List<Enemy_> bossEnemiesList;
    float totalBossHealth;
    public float currentBossHealth;
    //[SerializeField] GameObject bossHealthInfo;
    public Image bossHealthBar;
    [SerializeField] List<EnemiesSpawnGroup> enemiesSpawnGroupList = new List<EnemiesSpawnGroup>();
    [SerializeField] List<EnemiesSpawnGroup> repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
    
    int spawnPerFrame = 2;

    public AudioManager bossActionSound;

    private void Start()
    {
        bossActionSound = FindObjectOfType<AudioManager>();
        player = GameManager.instance.playerTransform.gameObject;
        bossHealthBar = FindObjectOfType<BossHealthBar>().GetComponent<Image>();
        //bossHealthBar = GameObject.Find("BossHealthBar").transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        stageProgress = FindObjectOfType<StageProgress>();       
    }

    private void Update()
    {
        ProcessSpawn();
        ProcessRepeatedSpawnGroup();
        UpdateBossHealth();
    }

    private void ProcessRepeatedSpawnGroup()
    {
        if (repeatedSpawnGroupList == null) { return; }
        for (int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
        {
            repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
            if (repeatedSpawnGroupList[i].repeatTimer < 0)
            {
                repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
                AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].count, repeatedSpawnGroupList[i].isBoss);
                repeatedSpawnGroupList[i].repeatCount -= 1;
                if (repeatedSpawnGroupList[i].repeatCount <= 0)
                {
                    repeatedSpawnGroupList.RemoveAt(i);
                }
            }
        }
    }

    private void ProcessSpawn()
    {
        if (enemiesSpawnGroupList == null) { return; }
        for (int i = 0; i<spawnPerFrame; i++) 
        {
            if (enemiesSpawnGroupList.Count > 0)
            {
                if (enemiesSpawnGroupList[0].count <= 0) { return; }
                SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
                enemiesSpawnGroupList[0].count -= 1;
                if (enemiesSpawnGroupList[0].count <= 0)
                {
                    enemiesSpawnGroupList.RemoveAt(0);
                }

            }
        }
        
    }
    private void UpdateBossHealth()
    {
        
        if (bossEnemiesList == null) { return; }
        if (bossEnemiesList.Count == 0) { return; }

        currentBossHealth = 0;

        for (int i = 0; i < bossEnemiesList.Count; i++)
        {
            if (bossEnemiesList[i] == null) { continue; }
            currentBossHealth += bossEnemiesList[i].stats.health;
        }
        bossHealthBar.fillAmount = currentBossHealth/totalBossHealth;
        //bossHealthBar.value = currentBossHealth;

        if (currentBossHealth <= 0)
        {
            bossHealthBar.gameObject.SetActive(false);
            bossEnemiesList.Clear();
        }
    }

    //public void DisplayBossHealth(bool toggle)
    //{
        //bossHealthInfo.SetActive(toggle);
    //}

    //private void InitializeBossHealthUI()
    //{
         //DisplayBossHealth(true);
         
    //}


    public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    {
        EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

        if (enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); }
        enemiesSpawnGroupList.Add(newGroupToSpawn);
    }

    public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)

    {
        Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);
        position += player.transform.position;

        GameObject newEnemy = Instantiate(enemyToSpawn.animatedPrefab);
        newEnemy.transform.position = position;
        Enemy_ newEnemyComponent = newEnemy.GetComponent<Enemy_>();
        newEnemyComponent.SetTarget(player);
        newEnemyComponent.SetStats(enemyToSpawn.stats);
        newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
        newEnemy.transform.parent = transform;

       
        

        if (isBoss == true)
        {
            bossActionSound.StopSound(28);
            bossActionSound.PlayOneShotAudio(5);
            bossActionSound.PlayEffect(39);
            bossActionSound.PlayEffect(6);
            SpawnBossEnemy(newEnemyComponent);
        }
    }

    

    private void SpawnBossEnemy(Enemy_ newBoss)
    {
        if (bossEnemiesList == null) 
            { 
                Debug.Log("No Boss");
                bossEnemiesList = new List<Enemy_>(); 
            }

        bossEnemiesList.Add(newBoss);
        Debug.Log("Spawn Boss");
        totalBossHealth += newBoss.stats.health;
        bossHealthBar.gameObject.SetActive(true);
        //bossHealthBar.maxValue = totalBossHealth;
        bossHealthBar.fillAmount = totalBossHealth/totalBossHealth;

    }

    public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    {
        EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
        repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);
        if (repeatedSpawnGroupList == null)
        {
            repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
        }

        repeatedSpawnGroupList.Add(repeatSpawnGroup);
    }
}