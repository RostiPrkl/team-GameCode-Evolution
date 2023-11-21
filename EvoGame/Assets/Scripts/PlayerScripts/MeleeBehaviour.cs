using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Script for melee Weapon behaviour

public class MeleeBehaviour : MonoBehaviour
{
    public float destroyCounterMelee;


    protected virtual void Start()
    {
        Destroy(gameObject, destroyCounterMelee);
    }
}
