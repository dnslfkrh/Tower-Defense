using System.Collections;
using UnityEngine;

public class Shotgun : MonoBehaviour, ITower
{
    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    [Header("Shotgun")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float attackRate = 1.5f; 
    [SerializeField] private float attackRange = 2.5f;
    [SerializeField] private int attackDamage = 3;
    [SerializeField] private int sellPrice = 35;
    [SerializeField] private float targetPersistenceTime = 0.5f;

    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;
    private float targetAcquiredTime;
    private Coroutine attackCoroutine;
    private bool isShooting = false;

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
            if (IsPossibleToAttackTarget() && !isShooting)
            {
                isShooting = true;
                yield return StartCoroutine(FireShots());
                yield return new WaitForSeconds(attackRate);
                isShooting = false;
            }
            yield return null;
        }
    }

    private IEnumerator FireShots()
    {
        TryAttack();
        yield return new WaitForSeconds(0.2f);
        TryAttack();
    }

    private void Update()
    {
        if (attackTarget == null || !IsTargetValid())
        {
            attackTarget = FindClosestAttackTarget();
            if (attackTarget != null)
            {
                targetAcquiredTime = Time.time;
            }
        }

        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        Vector3 direction = attackTarget.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;
        Transform closestTarget = null;

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

    private void TryAttack()
    {
        SpawnProjectile();
    }

    private void SpawnProjectile()
    {
        if (projectilePrefab != null && attackTarget != null)
        {
            GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
            clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
        }
    }

    private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null) return false;

        float distanceSqr = (attackTarget.position - transform.position).sqrMagnitude;
        bool inRange = distanceSqr <= attackRange * attackRange;

        return inRange;
    }

    private bool IsTargetValid()
    {
        if (attackTarget == null) return false;

        bool targetExists = enemySpawner.EnemyList.Exists(e => e != null && e.transform == attackTarget);

        if (!targetExists)
        {
            Debug.Log($"Target {attackTarget.name} no longer exists in EnemyList.");
            return false;
        }

        if (Time.time - targetAcquiredTime < targetPersistenceTime)
        {
            return true;
        }

        return IsPossibleToAttackTarget();
    }

    public void Sell()
    {
        playerGold.CurrentGold += sellPrice;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
