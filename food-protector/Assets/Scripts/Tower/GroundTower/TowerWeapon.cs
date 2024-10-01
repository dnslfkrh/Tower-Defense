using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WeaponType { Catapult = 0, Razer = 0}
public enum WeaponState { SearchTarget = 0, TryAttackCatapult }

public class TowerWeapon : MonoBehaviour
{
    [Header("Common")]
    [SerializeField]
    private Transform spawnPoint;
    
    [SerializeField]
    private WeaponType weaponType;

    [SerializeField]
    private float attackRate = 0.5f;

    [SerializeField]
    private float attackRange = 2.0f;

    [Header("Catapult")]
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private int attackDamage = 1; // 타워 데미지

    [SerializeField]
    private int sellPrice = 25; // 타워 판매 가격

    private WeaponState weaponState = WeaponState.SearchTarget;
    private Transform attackTarget = null;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;

    public float Damage => attackDamage;
    public float Rate => attackRate;
    public float Range => attackRange;

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;

        ChangeState(WeaponState.SearchTarget);
    }

    public void ChangeState(WeaponState newState)
    {
        StopCoroutine(weaponState.ToString());

        weaponState = newState;

        StartCoroutine(weaponState.ToString());
    }

    private void Update()
    {
        if (attackTarget != null)
        {
            RotateToTarget();
        }
    }

    private void RotateToTarget()
    {
        float dx = attackTarget.position.x - transform.position.x;
        float dy = attackTarget.position.y - transform.position.y;

        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, degree);
    }

    private IEnumerator SearchTarget()
    {
        while (true)
        {
            attackTarget = FindClosestAttackTarget();

            if (attackTarget != null)
            {
                ChangeState(WeaponState.TryAttackCatapult);
            }

            yield return null;
        }
    }

    //private IEnumerator SearchTarget()
    //{
    //    while (true)
    //    {
    //        float closestDistSqr = Mathf.Infinity;

    //        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
    //        {
    //            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

    //            if (enemySpawner.EnemyList[i].CompareTag("Ground") && distance <= attackRange && distance <= closestDistSqr) // 지상
    //            {
    //                closestDistSqr = distance;
    //                attackTarget = enemySpawner.EnemyList[i].transform;
    //            }
    //            else
    //            {
    //                yield return null;
    //            }
    //        }

    //        if (attackTarget != null)
    //        {
    //            ChangeState(WeaponState.TryAttackCatapult);
    //        }

    //        yield return null;
    //    }
    //}

    private Transform FindClosestAttackTarget()
    {
        float closestDistSqr = Mathf.Infinity;

        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
        {
            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

            if (enemySpawner.EnemyList[i].CompareTag("Ground") && distance <= attackRange && distance <= closestDistSqr) // 지상
            {
                closestDistSqr = distance;
                attackTarget = enemySpawner.EnemyList[i].transform;
            }

        }

        return attackTarget;
    }

    //private IEnumerator AttackToTarget()
    //{
    //    while (true)
    //    {
    //        if (attackTarget == null)
    //        {
    //            ChangeState(WeaponState.SearchTarget);
    //            break;
    //        }

    //        float distance = Vector3.Distance(attackTarget.position, transform.position);
    //        if (distance > attackRange)
    //        {
    //            attackTarget = null;
    //            ChangeState(WeaponState.SearchTarget);
    //            break;
    //        }

    //        yield return new WaitForSeconds(attackRate);

    //        SpawnProjectile();
    //    }
    //}

    private IEnumerator TryAttackCatapult()
    {
        while (true)
        {
            if (IsPossibleToAttackTarget() == false)
            {
                ChangeState(WeaponState.SearchTarget);
                break;
            }

            SpawnProjectile();

            yield return new WaitForSeconds(attackRate);
        }
    }

    private bool IsPossibleToAttackTarget()
    {
        if (attackTarget == null)
        {
            return false;
        }

        float distance = Vector3.Distance(attackTarget.position, transform.position);
        if (distance > attackRange)
        {
            attackTarget = null;
            ChangeState(WeaponState.SearchTarget);
        }

        return true;
    }

    private void SpawnProjectile()
    {
        //Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

        clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
    }
    
    public void Sell()
    {
        playerGold.CurrentGold += sellPrice;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
