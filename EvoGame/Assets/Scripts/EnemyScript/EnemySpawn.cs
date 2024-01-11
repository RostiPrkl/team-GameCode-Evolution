using System;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    
    [SerializeField] GameObject enemy;
    [SerializeField] Vector2 spawnArea;
    [SerializeField] float spawnTimer;
    [SerializeField] float initialSpawnTimer;
    [SerializeField] GameObject player;
    public float timer;


    private void Awake()
    {
        timer = initialSpawnTimer;
    }


    private void Update()

    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
            SpawnEnemy();
    }


    private void SpawnEnemy()

    {
        Vector3 position = GenerateRandomPosition();
        position += player.transform.position;
        timer = spawnTimer;

        GameObject newEnemy = Instantiate(enemy);
        newEnemy.transform.position = position;
        newEnemy.GetComponent<Enemy>();
        //Enemy newEnemyComponent = newEnemy.GetComponent<Enemy>();      
        newEnemy.transform.parent = transform;
    }

    private Vector3 GenerateRandomPosition()
{
    Vector3 position = new Vector3();

    float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;

    // Maximum attempts to find a valid position
    int maxAttempts = 10;
    int attempts = 0;
    bool hasEnvironmentCollider;

    do
    {
        if (UnityEngine.Random.value > 0.5f)
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);
            position.y = spawnArea.y * f;
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);
            position.x = spawnArea.x * f;
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(position, 180f);
        hasEnvironmentCollider = false;

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Environment"))
            {
                hasEnvironmentCollider = true;
                break;
            }
        }
                attempts++;

    } while (hasEnvironmentCollider && attempts < maxAttempts);

    position.z = 0;
    return position;
}
}
