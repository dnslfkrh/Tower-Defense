using System.Collections;
using UnityEngine;

public class Laser : MonoBehaviour, ITower
{
    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    [Header("Laser")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private Transform hitEffect;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private float attackRate = 0.5f;
    [SerializeField] private float attackRange = 5.0f;
    [SerializeField] private int attackDamage = 2;
    [SerializeField] private int sellPrice = 25;

    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;
    private Coroutine attackCoroutine;

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
                SpawnLaser();
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
        if (attackTarget != null && !IsPossibleToAttackTarget())
        {
            attackTarget = null;
            DisableLaser();
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

    private void SpawnLaser()
    {
        if (attackTarget == null)
        {
            DisableLaser();
            return;
        }

        Vector3 direction = attackTarget.position - transform.position;
        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, attackRange, targetLayer);

        bool hitTarget = false;
        foreach (var hitInfo in hits)
        {
            if (hitInfo.transform == attackTarget)
            {
                lineRenderer.SetPosition(0, spawnPoint.position);
                lineRenderer.SetPosition(1, hitInfo.point);
                hitEffect.position = hitInfo.point;
                hitEffect.gameObject.SetActive(true);

                attackTarget.GetComponent<EnemyHP>().TakeDamage(attackDamage * Time.deltaTime);
                hitTarget = true;
                break;
            }
        }

        if (!hitTarget)
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
