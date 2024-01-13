using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="AttackScriptableObject", menuName ="Player/PlayerAttack")]
public class PlayerAttackScriptableObject : ScriptableObject
{
    [SerializeField] GameObject attackPrefab;
    public GameObject AttackPrefab { get => attackPrefab; private set => attackPrefab = value; }
    [SerializeField] int damage;
    public int Damage { get => damage; private set => damage = value; }
    [SerializeField] float speed;
    public float Speed { get => speed; private set => speed = value; }
    [SerializeField] float cooldown;
    public float Cooldown { get => cooldown; private set => cooldown = value; }
}
