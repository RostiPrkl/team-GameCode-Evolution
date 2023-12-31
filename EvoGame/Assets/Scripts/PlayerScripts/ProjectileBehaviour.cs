using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base Script for projectile Weapon behaviour

public class ProjectileBehaviour : MonoBehaviour
{
    protected Vector3 direction;
    public float destroyCounterProjectile;
    public PlayerAttackScriptableObject attackData;

    protected float currentDamage;
    protected float currentSpeed;
    protected float currentCooldownDur;


    void Awake()
    {
        currentDamage = attackData.Damage;
        currentSpeed = attackData.Speed;
        currentCooldownDur = attackData.Cooldown;
    }


    protected virtual void Start()
    {
        Destroy(gameObject, destroyCounterProjectile);
    }

    
    public void DirectionCheck(Vector3 dir)
    {
        direction = dir;

        float dirX = direction.x;
        float dirY = direction.y;

        Vector3 scale = transform.localScale;
        Vector3 rotation = transform.rotation.eulerAngles;

        //temporary, switch to rigidbody velocity movement
        if (dirX > 0 && dirY == 0 ) //facing right
            rotation.z = -30f;
        else if (dirX < 0 && dirY == 0) //facing left
            rotation.z = 30f;
        else if (dirX == 0 && dirY == 0) //facing down
            scale.y *= 0;
        else if (dirX == 0 && dirY > 0) //facing up
            scale.y *= -1;
        else if (dirX > 0 && dirY > 0) //right up
            rotation.z = 0f;
        else if (dirX > 0 && dirY < 0) //right down
            rotation.z = -90f;
        else if (dirX < 0 && dirY > 0) //left up
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = -90f;
        }
        else if (dirX < 0 && dirY < 0) //left down
        {
            scale.x *= -1;
            scale.y *= -1;
            rotation.z = 0f;
        }
        //mmmm delicious spagetti

        transform.localScale = scale;
        transform.rotation = Quaternion.Euler(rotation);
    }


    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Enemy enemy = collider.GetComponent<Enemy>();
            enemy.TakeDamage(currentDamage);
            Destroy(gameObject);
        }
            
    }
}
