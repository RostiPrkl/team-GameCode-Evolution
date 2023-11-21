using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits from projectile behaviour

public class ShoutBehaviour : ProjectileBehaviour
{
    //reference shout controller
    ShoutController sc;
    
    protected override void Start()
    {
        base.Start();
        sc = FindObjectOfType<ShoutController>();
    }

    
    void Update()
    {
        //set the movement of shout
        transform.position += direction * sc.speed * Time.deltaTime;
    }
}
