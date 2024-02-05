using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits from projectile behaviour

public class ShoutBehaviour : ProjectileBehaviour
{
    //reference shout controller
    Transform target;
    protected Rigidbody2D rb;
    float rotationSpeed = 200f;
    bool targetReached = false;
    
    
    protected override void Start()
    {
        base.Start();
        target = GameObject.FindGameObjectWithTag("Enemy").transform;
        rb = GetComponent<Rigidbody2D>();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;

        GameObject targetObject = new GameObject("Target");
        targetObject.transform.position = mousePosition;
        target = targetObject.transform;

        Destroy(targetObject, 3f);
    }

    
    protected override void Update()
    {
        base.Update();
        TargetingSystem();
    }


    void TargetingSystem()
    {
        if (!targetReached && transform != null)
        {
            Vector3 direction = target.position - transform.position;

            float step = rotationSpeed * Time.deltaTime;
            Vector3 newDir = Vector3.RotateTowards(transform.up, direction, step, 0.0f);
            transform.up = newDir;

            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, target.position) < 0.1f)
            {
                targetReached = true;
            }
        }
        if (targetReached && transform != null)
            transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);
    }
}
