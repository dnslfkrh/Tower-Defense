using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Transform target;
    private int damage;
    public float explosionRadius = 3.0f;
    public int explosionDamage = 10;
    public LayerMask enemyLayer;

    public void Setup(Transform target, int damage)
    {
        this.target = target;
        this.damage = damage;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector3 direction = (target.position - transform.position).normalized;
            float moveSpeed = 7f * Time.deltaTime;
            transform.position += direction * moveSpeed;

            if (Vector3.Distance(transform.position, target.position) < 0.2f)
            {
                StartCoroutine(DelayedExplode());
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
        if (!collision.CompareTag("Ground"))
        {
            return;
        }

        if (collision.transform != target)
        {
            return;
        }

        collision.GetComponent<EnemyHP>().TakeDamage(damage);
        StartCoroutine(DelayedExplode()); 
    }

    private IEnumerator DelayedExplode()
    {
        yield return new WaitForSeconds(2f);
        Explode(); 
    }

    private void Explode()
    {
        Debug.Log("Æã~~~");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, explosionRadius, enemyLayer);
        foreach (Collider2D enemy in hitEnemies)
        {
            EnemyHP enemyHP = enemy.GetComponent<EnemyHP>();
            if (enemyHP != null)
            {
                enemyHP.TakeDamage(explosionDamage);
            }
        }

        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
