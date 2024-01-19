using UnityEngine;
//Base for all player attack types to inherit from

public class PlayerAttackController : MonoBehaviour
{
    [Header("Attack Stats")]
    public PlayerAttackScriptableObject attackData;
    protected PlayerStats player;
    protected Player playerMovement;
    float currentCooldown;



    protected virtual void Start()
    {
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
