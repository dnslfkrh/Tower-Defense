using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour, ITower
{
    [SerializeField]
    private Transform spawnPoint;
    
    [SerializeField]
    private LineRenderer lineRenderer;
    
    [SerializeField]
    private Transform hitEffect;
    
    [SerializeField]
    private LayerMask targetLayer;
    
    [SerializeField]
    private float attackRate = 0.5f;
    
    [SerializeField]
    private float attackRange = 5.0f;
    
    [SerializeField]
    private int attackDamage = 2;
    
    [SerializeField]
    private int sellPrice = 25;

    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;
    private Coroutine attackCoroutine;

    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;

        attackCoroutine = StartCoroutine(AttackCoroutine());
    }

    private IEnumerator AttackCoroutine()
    {
        while (true)
        {
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                RotateToTarget();
                EnableLaser();
            }
            else
            {
                DisableLaser();
            }

            yield return new WaitForSeconds(attackRate);
        }
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            if (IsPossibleToAttackTarget())
            {
                RotateToTarget();
                UpdateLaser();
            }
            else
            {
                attackTarget = null;
                DisableLaser();
            }
        }
    }

    private void RotateToTarget()
    {
        if (attackTarget != null)
        {
            Vector3 direction = attackTarget.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }

    private Transform FindClosestAttackTarget()
    {
        Transform closestTarget = null;
        float closestDistSqr = Mathf.Infinity;

        foreach (var enemy in enemySpawner.EnemyList)
        {
            if (enemy == null) continue;

            float distanceSqr = (enemy.transform.position - transform.position).sqrMagnitude;
            if (enemy.CompareTag("Ground") && distanceSqr <= attackRange * attackRange && distanceSqr < closestDistSqr)
            {
                closestDistSqr = distanceSqr;
                closestTarget = enemy.transform;
            }
        }

        return closestTarget;
    }

    private void UpdateLaser()
    {
        if (attackTarget == null)
        {
            DisableLaser();
            return;
        }

        Vector3 direction = attackTarget.position - transform.position;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, attackRange, targetLayer);

        if (hit.collider != null && hit.transform == attackTarget)
        {
            lineRenderer.SetPosition(0, spawnPoint.position);
            lineRenderer.SetPosition(1, hit.point);
            hitEffect.position = hit.point;
            hitEffect.gameObject.SetActive(true);

            attackTarget.GetComponent<EnemyHP>().TakeDamage(attackDamage * Time.deltaTime);
        }
        else
        {
            DisableLaser();
        }
    }

    private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null) return false;

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        return distance <= attackRange;
    }

    private void EnableLaser()
    {
        lineRenderer.gameObject.SetActive(true);
        hitEffect.gameObject.SetActive(true);
    }

    private void DisableLaser()
    {
        lineRenderer.gameObject.SetActive(false);
        hitEffect.gameObject.SetActive(false);
    }

    public void Sell()
    {
        playerGold.CurrentGold += sellPrice;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}