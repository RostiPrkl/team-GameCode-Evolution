using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Enemy", menuName ="Object/Enemy")]
public class EnemyData : ScriptableObject
{
    //public string Name;
    [SerializeField] GameObject enemyPrefab;
    public GameObject EnemyPrefabe { get => enemyPrefab; private set => enemyPrefab = value; }
    //public EnemyStats stats;
    [SerializeField] float health;
    public float Health { get => health; private set => health = value; }
    [SerializeField] float moveSpeed;
    public float MoveSpeed { get => moveSpeed; private set => moveSpeed = value; }
    [SerializeField] float damage;
    public float Damage { get => damage; private set => damage = value; }
    

}
