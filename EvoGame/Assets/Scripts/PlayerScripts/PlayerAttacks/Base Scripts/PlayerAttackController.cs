using UnityEngine;
//Base for all player attack types to inherit from

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Stats")]
    public PlayerAttackScriptableObject attackData;
    protected PlayerStats player;
    //protected Player playerMovement;
    float currentCooldown;
   // public SoundController shout;
    public AudioManager attackSound;



    protected virtual void Start()
    {
        attackSound = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<PlayerStats>();
        
        currentCooldown = attackData.Cooldown;
    }


    protected virtual void Update()
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0f)
            Attack();
    }


    protected virtual void Attack()
    {

        currentCooldown = attackData.Cooldown;
    }
}
