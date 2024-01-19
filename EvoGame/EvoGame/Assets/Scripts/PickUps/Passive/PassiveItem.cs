using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItem;


    protected virtual void Modifier()
    {
        //just a initializon of the method, 
        //the actual modifier takes place in the child class
    }


    void Start()
    {
        player = FindObjectOfType<PlayerStats>(); 
        Modifier();
    }
}
