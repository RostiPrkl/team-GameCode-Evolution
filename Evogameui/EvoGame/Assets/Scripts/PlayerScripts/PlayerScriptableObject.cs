using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerScriptableObject", menuName ="Player/PlayerCharacter")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] GameObject startingWeapon;
    public GameObject StartingWeapon { get => startingWeapon; private set => startingWeapon = value; }

    [SerializeField] float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField] float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }
    
    [SerializeField] float recovery;
    public float Recovery { get => recovery; private set =>  recovery= value; }
    
    [SerializeField] float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }

}