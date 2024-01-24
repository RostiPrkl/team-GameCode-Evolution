using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EnemyFlash : MonoBehaviour
{
    public Color damageColor = new Color(0,0,0,0.6f);
    public float flashDuration = 0.2f;
    public float deathFade = 0.6f;

    Color originalColor;
    SpriteRenderer sr;
    Enemy_ enemy;

    void Start()
    {
        enemy = GetComponent<Enemy_>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
    }


    void Update()
    {
        if (enemy.isDamaged)
            StartCoroutine(DamageFlash());

        if (enemy.stats.health <= 0f)
            StartCoroutine(KillFade());
        
    }


    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(flashDuration);
        sr.color = originalColor;
    }


    IEnumerator KillFade()
    {
        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0, originalAlpha = sr.color.a;
        while (t < deathFade)
        {
            yield return w;
            t += Time.deltaTime;
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFade) * originalAlpha);
        }
    }


}
