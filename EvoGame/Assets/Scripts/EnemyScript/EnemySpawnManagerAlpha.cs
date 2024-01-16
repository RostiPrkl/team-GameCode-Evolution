using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManagerAlpha : MonoBehaviour
{
    [SerializeField] float spawnRate = 1f;
    [SerializeField] GameObject[] enemies;
    bool canSpawn = true;
    

    void Start()
    {
        StartCoroutine(NextEnemy());
    }


    void Update()
    {
        
    }


    IEnumerator NextEnemy()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate);
        while (canSpawn)
        {
            yield return wait;
        }
    }
}
