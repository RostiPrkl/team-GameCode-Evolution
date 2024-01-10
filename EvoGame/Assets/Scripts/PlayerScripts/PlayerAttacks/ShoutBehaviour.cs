using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Inherits from projectile behaviour

public class ShoutBehaviour : ProjectileBehaviour
{
    //reference shout controller
    
    
    protected override void Start()
    {
        base.Start();
    }

    
    protected override void Update()
    {
        base.Update();
        //set the movement of shout
        transform.position += direction * currentSpeed * Time.deltaTime;
    }
}
