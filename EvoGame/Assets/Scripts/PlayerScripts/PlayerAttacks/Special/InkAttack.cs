using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InkAttack : MonoBehaviour
{
    //magic number hack fuck
    private List<Enemy_> enemies = new List<Enemy_>();
    [SerializeField] float slowTime = 3f;


    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy_ enemy = other.GetComponent<Enemy_>();
            if (enemy != null && !enemies.Contains(enemy))
            {
                enemies.Add(enemy);
                enemy.stats.moveSpeed = 0.8f;
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Enemy_ enemy = other.GetComponent<Enemy_>();
            if (enemy != null && enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
                StartCoroutine(InkExitTimer(enemy));
            }
        }
    }

    IEnumerator InkExitTimer(Enemy_ enemy)
    {
        yield return new WaitForSeconds(slowTime);
        if (enemy != null)
        {
            enemy.stats.moveSpeed *= 2;
        }
    }
}

