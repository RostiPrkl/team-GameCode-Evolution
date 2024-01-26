using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Gaskellgames.AudioController;
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
    public SoundController biteEffects;


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
        biteEffects = FindObjectOfType<SoundController>();
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
                        biteEffects.PlaySoundFX("bite01");
                        break;
                    case "Stronger Bite":
                        biteEffects.PlaySoundFX("bite02");
                        break;
                    case "Bigger Teeth":
                        biteEffects.PlaySoundFX("bite03");
                        break;
                    case "Poison Teeth":
                        biteEffects.PlaySoundFX("bitePoison");
                        break;
                   /* default:
                        Debug.Log("NULL STATE");
                        break;*/
                }
                //audioSource.PlayOneShot(audioSource.clip);
            }

            

            playerMovement.animator.SetTrigger("Bite");
            Enemy_ enemy = collider.GetComponent<Enemy_>();
            enemy.TakeDamage(GetCurrentDamage());

            if (biteEffects.IsInvoking("PlaySoundFX"))
            {
                DestroyAfter();

            }
                
            //float delay = audioSource.clip.length;
            //Invoke("DestroyAfterDelay", delay);

        }
    }   


    private void DestroyAfter()
     {
            Destroy(gameObject);
            isPlaying = false;
        
    }

    /*private void DestroyAfterDelay()
     {
         Destroy(gameObject);
         isPlaying = false;
     }*/
}
