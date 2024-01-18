using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.RestService;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    //protected PlayerScriptableObject playerData;
    public PassiveItemScriptableObject passiveItem;


    protected virtual void Modifier()
    {
        //Initializon of the method, 
        //the actual modifier takes place in the child class
    }


    void Start()
    {
        //playerData = FindObjectOfType<PlayerScriptableObject>();
        player = FindObjectOfType<PlayerStats>(); 
        Modifier();
    }


    protected virtual void Update()
    {
        //Initialization of uppdate
    }
}
