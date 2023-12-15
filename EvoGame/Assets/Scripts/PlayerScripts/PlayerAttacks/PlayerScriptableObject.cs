
using UnityEngine;

[CreateAssetMenu(fileName ="PlayerScriptableObject", menuName ="Player/PlayerCharacter")]
public class PlayerScriptableObject : ScriptableObject
{
    [SerializeField] GameObject startingAttack;
    public GameObject StartingAttack { get => startingAttack; private set => startingAttack = value; }

    [SerializeField] float maxHealth;
    public float MaxHealth { get => maxHealth; private set => maxHealth = value; }
    
    [SerializeField] float projectileSpeed;
    public float ProjectileSpeed { get => projectileSpeed; private set => projectileSpeed = value; }

    [SerializeField] float recovery;
    public float Recovery { get => recovery; private set =>  recovery= value; }
    
    [SerializeField] float movementSpeed;
    public float MovementSpeed { get => movementSpeed; private set => movementSpeed = value; }

    [SerializeField] float pickupRadius;
    public float PickupRadius { get => pickupRadius; private set => pickupRadius = value; }

    [SerializeField] float baseDamage;
    public float BaseDamage { get => baseDamage; private set => baseDamage = value; }

}