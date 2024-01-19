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
    [SerializeField] AudioSource audioSource;
    bool isPlaying;


    public SoundController meleeSound;


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
        meleeSound = FindObjectOfType<SoundController>();
        Destroy(gameObject, destroyCounterMelee);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            if (!isPlaying)
            {
                isPlaying = true;
                
                if (attackData.AttackName == "Bite") 
                {
                    meleeSound.PlaySoundFX("bite01");
                    //audioSource.PlayOneShot(audioSource.clip);
                }
                if (attackData.AttackName == "Stronger Teeth")
                {
                    meleeSound.PlaySoundFX("bite02");
                    //audioSource.PlayOneShot(audioSource.clip);
                }
                if (attackData.AttackName == "MAX TEETH")
                {
                    meleeSound.PlaySoundFX("bite03");
                    // audioSource.PlayOneShot(audioSource.clip);
                }

            }
            Enemy_ enemy = collider.GetComponent<Enemy_>();
            enemy.TakeDamage(GetCurrentDamage());

            /*float delay = audioSource.clip.length;
            Invoke("DestroyAfterDelay", delay);*/
        }
    }


    private void DestroyAfterDelay()
    {
        Destroy(gameObject);
        isPlaying = false;
    }
}
