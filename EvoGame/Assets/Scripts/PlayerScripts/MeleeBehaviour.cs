using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Script for melee Weapon behaviour

public class MeleeBehaviour : MonoBehaviour
{
    public float destroyCounterMelee;
    public PlayerAttackScriptableObject attackData;

    public float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDur;
    [SerializeField] AudioSource audioSource;


    void Awake()
    {
        currentDamage = attackData.Damage;
        currentSpeed = attackData.Speed;
        currentCooldownDur = attackData.Cooldown;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destroyCounterMelee);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            audioSource.PlayOneShot(audioSource.clip);
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(currentDamage);

            float delay = audioSource.clip.length;
            Invoke("DestroyAfterDelay", delay);
        }
    }


    private void DestroyAfterDelay()
    {
        Destroy(gameObject);
    }
}
