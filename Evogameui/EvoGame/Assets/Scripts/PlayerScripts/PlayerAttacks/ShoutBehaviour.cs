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

    
    void Update()
    {
        //set the movement of shout
        transform.position += direction * attackData.Speed * Time.deltaTime;
    }
}
