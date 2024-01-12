using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//designed to manage the spawning of objects 
public class SpawnManager : MonoBehaviour
{   //used to create a singleton pattern, ensuring that there is only one instance of the SpawnManager in the game.
    public static SpawnManager instance;

    private void Awake()
    {
        instance = this;    //Sets the instance variable to refer to the current instance of the SpawnManager.
    }
    //that allows the spawning of objects at a specified world position.
    public void SpawnObject(Vector3 worldPosition, GameObject toSpawn)
    {
        Transform t = Instantiate(toSpawn, transform).transform;    //Instantiate function to create a new instance of the specified toSpawn GameObject.
        t.position = worldPosition; //Sets the position of the instantiated object to the specified worldPosition.
    }
}
