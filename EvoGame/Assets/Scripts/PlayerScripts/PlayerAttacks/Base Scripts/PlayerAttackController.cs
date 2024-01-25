using UnityEngine;
using Gaskellgames.AudioController;
//Base for all player attack types to inherit from

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Stats")]
    public PlayerAttackScriptableObject attackData;
    protected PlayerStats player;
    //protected Player playerMovement;
    float currentCooldown;
    public SoundController shout;



    protected virtual void Start()
    {
        shout = FindObjectOfType<SoundController>();
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

        switch (attackData.AttackName)
        {
            case "Water Bullet":
                shout.PlaySoundFX("shoot01");
                break;
            case "Bubbles 2":
                shout.PlaySoundFX("shoot02");
                break;
            case "Bubble 3":
                shout.PlaySoundFX("shoot03");
                break;
            case "Vortex":
                shout.PlaySoundFX("shootVortex");
                break;
           /* default:
                Debug.Log("NULL STATE");
                break;*/
        }
        currentCooldown = attackData.Cooldown;
    }
}
