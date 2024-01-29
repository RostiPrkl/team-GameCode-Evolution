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
    //[SerializeField] AudioSource audioSource;
    Player playerMovement;
    //public Animator animator;
    bool isPlaying;
    //public SoundController biteEffects;
    public AudioManager biteEffects;


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
        biteEffects = FindObjectOfType<AudioManager>();
        playerMovement = FindObjectOfType<Player>();
        Destroy(gameObject, destroyCounterMelee);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (!isPlaying)
            {
                isPlaying = true;

                switch (attackData.AttackName)
                {
                    case "Bite":
                        biteEffects.PlayEffect(2);
                        break;
                    case "Stronger Bite":
                        biteEffects.PlayEffect(3);
                        break;
                    case "Bigger Teeth":
                        biteEffects.PlayEffect(4);
                        break;
                    case "Poison Teeth":
                        biteEffects.PlayEffect(20);
                        break;
                   /* default:
                        Debug.Log("NULL STATE");
                        break;*/
                }
                //audioSource.PlayOneShot(audioSource.clip);
            }

            

            playerMovement.animator.SetTrigger("Bite");
            Enemy_ enemy = collider.GetComponent<Enemy_>();

            if (enemy.isDamaged == false)
            {
                enemy.TakeDamage(GetCurrentDamage());
                enemy.isDamaged = true;
            }

        }
    }   

}
