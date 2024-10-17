using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firework : MonoBehaviour
{
    private float damage;
    public float explosionRadius = 2.0f;
    public float explosionDamage = 15.0f;
    private Transform target;
    public LayerMask enemyLayer;

    public void Setup(Transform target, float damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float moveSpeed = 10f * Time.deltaTime;
            transform.position += direction * moveSpeed;

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                Explode();
            }
        }
        else
        {
            Debug.Log("Å¸°ÙÀÌ ¾ø³×");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Air"))
        {
            return;
        }

        if (collision.transform != target)
        {
            return;
        }

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        Explode();
    }

    private void Explode()
    {
        Debug.Log("ÆøÁ× Æø¹ß!");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHP enemyHP = enemy.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(explosionDamage);
            }
        }

        StartCoroutine(DestroyAfterDelay(0.1f));
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
