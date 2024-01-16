//using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawn : MonoBehaviour
{
    [System.Serializable]
    public class EnemiesSpawnGroup
    {
        public string waveName;
        public List<GrupInWave> enemyGroups;
        public int quota;
        public int spawnCount;
        public float timeBetweenSpawn;
        
        // public EnemiesSpawnGroup(EnemyData enemyData, int count, bool isBoss)
        // {
        //     this.enemyData = enemyData;
        //     this.enemyCount = count;
        //     this.isBoss = isBoss;
        // }
        // public void SetRepeatSpawn(float timeBetweenSpawn, int repeatCount)
        // {
        //     this.timeBetweenSpawn = timeBetweenSpawn;
        //     this.repeatCount = repeatCount;
        //     repeatTimer = timeBetweenSpawn;

        // }
    }

    [System.Serializable]
    public class GrupInWave
    {
        public string EnemyName;
        public int enemyCount;
        public int spawnCount;
        public GameObject enemyPrefab;
        public bool isBoss;
        [SerializeField] Slider bossHealthBar;

    }

    //[SerializeField] StageProgress stageProgress;
    //[SerializeField] GameObject enemy;    
    //[SerializeField] Vector2 spawnArea;
    [SerializeField] Transform player;

    public int waveCount;

    List<Enemy_> bossEnemiesList;
    public List<EnemiesSpawnGroup> waves;
    float totalBossHealth;
    float currentBossHealth;
    List<EnemiesSpawnGroup> enemiesSpawnGroupList;
    List<EnemiesSpawnGroup> repeatedSpawnGroupList;

    public float repeatTimer;
    public float waveTimer;
    public int maxEnemies;
    public int enemiesAlive;
    bool isFull = false;
    bool isCoroutineRunning = false;
    //public int repeatCount;

    int spawnPerFrame = 2;



    private void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
    //     bossHealthBar = FindObjectOfType<BossHealthBar>(true).GetComponent<Slider>();
    //     stageProgress = FindObjectOfType<StageProgress>();
    // }

    // private void Update()
    // {
    //     ProcessSpawn();
    //     ProcessRepeatedSpawnGroup();
    //     UpdateBossHealth();
        CalculateWaveQuota();
        repeatTimer = 0f;
    }


    void Update()
    {
        repeatTimer += Time.deltaTime;
        
        if (waveCount < waves.Count && waves[waveCount].spawnCount == 0 && !isCoroutineRunning)
            StartCoroutine(NextWave());

        if (repeatTimer >= waves[waveCount].timeBetweenSpawn)
        {
            repeatTimer = 0f;
            SpawnEnemies();
        }
    }


    IEnumerator NextWave()
    {
        isCoroutineRunning = true;

        yield return new WaitForSeconds(waveTimer);

        if (waveCount < waves.Count - 1)
        {
            isCoroutineRunning = false;
            waveCount++;
            CalculateWaveQuota();
        }
    }


    void CalculateWaveQuota()
    {
        int currentWave = 0;
        foreach (var group in waves[waveCount].enemyGroups)
        {
            currentWave += group.enemyCount;
        }

        waves[waveCount].quota = currentWave;
        Debug.LogWarning(currentWave);
    }
    // private void ProcessRepeatedSpawnGroup()
    // {
    //     if (repeatedSpawnGroupList == null) { return; }
    //     for (int i = repeatedSpawnGroupList.Count - 1; i >= 0; i--)
    //     {
    //         repeatedSpawnGroupList[i].repeatTimer -= Time.deltaTime;
    //         if (repeatedSpawnGroupList[i].repeatTimer < 0)
    //         {
    //             repeatedSpawnGroupList[i].repeatTimer = repeatedSpawnGroupList[i].timeBetweenSpawn;
    //             AddGroupToSpawn(repeatedSpawnGroupList[i].enemyData, repeatedSpawnGroupList[i].enemyCount, repeatedSpawnGroupList[i].isBoss);
    //             repeatedSpawnGroupList[i].repeatCount -= 1;
    //             if (repeatedSpawnGroupList[i].repeatCount <= 0)
    //             {
    //                 repeatedSpawnGroupList.RemoveAt(i);
    //             }
    //         }
    //     }
    // }

    void SpawnEnemies()
    {
        if (waves[waveCount].spawnCount < waves[waveCount].quota && !isFull)
        {
            foreach (var enemyGroup in waves[waveCount].enemyGroups)
            {
                if (enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    Vector2 spawnPosition = new Vector2(player.position.x + Random.Range(-20f, 20f), player.position.y + Random.Range(-20f, 20f));
                    Instantiate(enemyGroup.enemyPrefab, spawnPosition, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[waveCount].spawnCount++;
                    enemiesAlive++;

                    if (enemiesAlive >= maxEnemies)
                    {
                        isFull = true;
                        return;
                    }
                }
            }
        }
    }


    public void OnEnemyKIlled()
    {
        enemiesAlive--;

        if (enemiesAlive < maxEnemies)
            isFull = false;
    }

    // private void ProcessSpawn()
    // {
    //     if (enemiesSpawnGroupList == null) { return; }
    //     for (int i = 0; i<spawnPerFrame; i++) 
    //     {
    //         if (enemiesSpawnGroupList.Count > 0)
    //         {
    //             if (enemiesSpawnGroupList[0].enemyCount <= 0) { return; }
    //             SpawnEnemy(enemiesSpawnGroupList[0].enemyData, enemiesSpawnGroupList[0].isBoss);
    //             enemiesSpawnGroupList[0].enemyCount -= 1;
    //             if (enemiesSpawnGroupList[0].enemyCount <= 0)
    //             {
    //                 enemiesSpawnGroupList.RemoveAt(0);
    //             }

    //         }
    //     }
        
    // }
    // private void UpdateBossHealth()
    // {
    //     if (bossEnemiesList == null) { return; }
    //     if (bossEnemiesList.Count == 0) { return; }

    //     currentBossHealth = 0;

    //     for (int i = 0; i < bossEnemiesList.Count; i++)
    //     {
    //         if (bossEnemiesList[i] == null) { continue; }
    //         currentBossHealth += bossEnemiesList[i].enemyData.Health;
    //     }
    //     bossHealthBar.value = currentBossHealth;

    //     if (currentBossHealth < 0)
    //     {
    //         bossHealthBar.gameObject.SetActive(false);
    //         bossEnemiesList.Clear();
    //     }
    // }

    // public void AddGroupToSpawn(EnemyData enemyToSpawn, int count, bool isBoss)
    // {
    //     EnemiesSpawnGroup newGroupToSpawn = new EnemiesSpawnGroup(enemyToSpawn, count, isBoss);

    //     if (enemiesSpawnGroupList == null) { enemiesSpawnGroupList = new List<EnemiesSpawnGroup>(); }
    //     enemiesSpawnGroupList.Add(newGroupToSpawn);
    // }

    // public void SpawnEnemy(EnemyData enemyToSpawn, bool isBoss)

    // {
    //     Vector3 position = UtilityTools.GenerateRandomPositionSquarePattern(spawnArea);
    //     position += player.transform.position;

    //     GameObject newEnemy = Instantiate(enemy);
    //     newEnemy.transform.position = position;
    //     Enemy_ newEnemyComponent = newEnemy.GetComponent<Enemy_>();
    //     newEnemyComponent.SetTarget(player);
    //     //newEnemyComponent.SetStats(enemyToSpawn.stats);
    //     //newEnemyComponent.UpdateStatsForProgress(stageProgress.Progress);
    //     newEnemy.transform.parent = transform;

       
        

    //     if (isBoss == true)
    //     {
    //         SpawnBossEnemy(newEnemyComponent);
    //     }
    // }

    

    // private void SpawnBossEnemy(Enemy_ newBoss)
    // {
    //     if (bossEnemiesList == null) { bossEnemiesList = new List<Enemy_>(); }

    //     bossEnemiesList.Add(newBoss);

    //     totalBossHealth += newBoss.enemyData.Health;
    //     bossHealthBar.gameObject.SetActive(true);
    //     bossHealthBar.maxValue = totalBossHealth;
    // }

    // public void AddRepeatedSpawn(StageEvent stageEvent, bool isBoss)
    // {
    //     EnemiesSpawnGroup repeatSpawnGroup = new EnemiesSpawnGroup(stageEvent.enemyToSpawn, stageEvent.count, isBoss);
    //     repeatSpawnGroup.SetRepeatSpawn(stageEvent.repeatEverySeconds, stageEvent.repeatCount);
    //     if (repeatedSpawnGroupList == null)
    //     {
    //         repeatedSpawnGroupList = new List<EnemiesSpawnGroup>();
    //     }

    //     repeatedSpawnGroupList.Add(repeatSpawnGroup);
    // }
}