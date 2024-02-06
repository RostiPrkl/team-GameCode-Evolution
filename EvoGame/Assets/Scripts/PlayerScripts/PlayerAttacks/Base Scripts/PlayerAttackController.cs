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
    public AudioManager shoutSound;



    protected virtual void Start()
    {
        shoutSound = FindObjectOfType<AudioManager>();
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

        switch (attackData.AttackPrefab.name)
        {
            case "ShoutWeapon 0":
                    shoutSound.PlayEffect(21);
                break;
            case "ShoutWeapon 1":
                shoutSound.PlayEffect(22);
                break;
            case "ShoutWeapon 2":
                shoutSound.PlayEffect(23);
                break;
            case "ShoutWeapon 3":
                shoutSound.PlayEffect(27);
                break;
           /* default:
                Debug.Log("NULL STATE");
                break;*/
        }
        currentCooldown = attackData.Cooldown;
    }
}
