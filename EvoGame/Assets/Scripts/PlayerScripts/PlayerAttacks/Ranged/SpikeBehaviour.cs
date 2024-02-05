using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBehaviour : ProjectileBehaviour
{

    public float rotationSpeed = 5f;

    private Transform target;
    private bool targetReached = false;
    //private Rigidbody2D rb;


    protected override void Start()
    {
        base.Start();
        //rb = GetComponent<Rigidbody2D>();
        PickRandomEnemy();
    }


    protected override void Update()
    {
        base.Update();
        TargetingSystem();
    }


    void PickRandomEnemy()
    {
        targetReached = false;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length > 0)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            target = enemies[randomIndex].transform;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    void TargetingSystem()
    {
        if (target != null)
        {
            if (!targetReached)
            {
                Vector3 direction = target.position - transform.position;

                float step = rotationSpeed * Time.deltaTime;
                Vector3 newDir = Vector3.RotateTowards(transform.up, direction, step, 0.0f);
                transform.up = newDir;

                transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, target.position) <= 0f)
                {
                    targetReached = true;
                    PickRandomEnemy();
                }
            }
            else
            {
                targetReached = false;
            }
        }
        else
        {
            
            Destroy(gameObject);
        }
    }

}
