using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform playerCamera;
    [SerializeField] Transform middleBG;
    [SerializeField] Transform sideBG;
    [SerializeField] float parallaxEffect;


    void Update()
    {
        if (playerCamera.position.x > middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.right * parallaxEffect;
        else if (playerCamera.position.x < middleBG.position.x)
            sideBG.position = middleBG.position + Vector3.left * parallaxEffect;

        if (playerCamera.position.x > sideBG.position.x || playerCamera.position.x < sideBG.position.x)
        {
            Transform newMiddleBG = middleBG;
            middleBG = sideBG;
            sideBG = newMiddleBG;
        }
    }   
}
