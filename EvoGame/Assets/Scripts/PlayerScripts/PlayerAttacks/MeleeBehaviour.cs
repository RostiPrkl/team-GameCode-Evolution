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
    //public Animator animator;
    bool isPlaying;


    void Awake()
    {
        currentDamage = attackData.Damage;
        currentSpeed = attackData.Speed;
        currentCooldownDur = attackData.Cooldown;
    }


    public float GetCurrentDamage()
    {
        return currentDamage *= FindObjectOfType<PlayerStats>().CurrentBaseDamage;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destroyCounterMelee);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (!isPlaying)
            {
                isPlaying = true;
                audioSource.PlayOneShot(audioSource.clip);
            }
            //animator.SetTrigger("Bite");
            Enemy_ enemy = collider.GetComponent<Enemy_>();
            enemy.TakeDamage(GetCurrentDamage());

            float delay = audioSource.clip.length;
            Invoke("DestroyAfterDelay", delay);
        }
    }


    private void DestroyAfterDelay()
    {
        Destroy(gameObject);
        isPlaying = false;
    }
}
