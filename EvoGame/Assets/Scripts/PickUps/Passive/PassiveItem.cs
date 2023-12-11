using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItem;


    protected virtual void Modifier()
    {

    }


    void Start()
    {
        player = FindObjectOfType<PlayerStats>(); 
        Modifier();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
