using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    ParticleSystem bombEffect;
    CircleCollider2D hitCol;

    void Start()
    {
        bombEffect = GetComponent<ParticleSystem>();
        hitCol = GetComponent<CircleCollider2D>();

        bombEffect.Play();
        StartCoroutine(bombMovement(1f));
    }

    IEnumerator bombMovement(float duration)
    {
        var shape = bombEffect.shape;
        float timer = 0f;

        while (timer <= duration)
        {
            float sizeLerp = Mathf.Lerp(0.5f, 20f, timer / duration);

            shape.radius = sizeLerp;
            hitCol.radius = sizeLerp;

            timer += Time.deltaTime;
            yield return null;
        }
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            var enemy = collision.GetComponent<EnemyBase>();
            enemy.OnDamage(100);
        }
    }
}
