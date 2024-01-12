using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//designed to generate a random position within a square pattern.
public class UtilityTools
{
    //generates a random position within a square pattern.
    public static Vector3 GenerateRandomPositionSquarePattern(Vector2 spawnArea)
    {
            
        Vector3 position = new Vector3();                                       //Initializes a Vector3 variable named position to store the generated random position.

        float f = UnityEngine.Random.value > 0.5f ? -1f : 1f;                   //Generates a random float f that is either -1 or 1 based on a random value. This is used to determine the sign of the position.
        if (UnityEngine.Random.value > 0.5f)                                    //Checks if another random value is greater than 0.5 to determine whether to generate the position along the x-axis or y-axis.
        {
            position.x = UnityEngine.Random.Range(-spawnArea.x, spawnArea.x);   //Generates a random x-coordinate within the specified range if the condition is true.
            position.y = spawnArea.y * f;                                       //If the condition is true, sets the y-coordinate to spawnArea.y multiplied by the previously generated sign f.
        }
        else
        {
            position.y = UnityEngine.Random.Range(-spawnArea.y, spawnArea.y);   //If the condition is false, generates a random y-coordinate within the specified range
            position.x = spawnArea.x * f;                                       //sets the x-coordinate to spawnArea.x multiplied by the sign f.
        }

        position.z = 0;                                                         //Sets the z-coordinate to 0
        return position;                                                        //Returns the generated Vector3 position.


    }
}
