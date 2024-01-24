using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexBehavior : MonoBehaviour
{
    public float pullForce = 300f; 
    public float extraDamage = 5f;


    private void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            Vector2 forceDir = (transform.position - collider.transform.position).normalized;
            rb.AddForce(forceDir * pullForce);
            Enemy_ enemy = collider.GetComponent<Enemy_>();
            enemy.TakeDamage(extraDamage);
        }
    }
}
