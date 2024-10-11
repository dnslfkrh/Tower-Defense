//using System.Collections;
//using System.Collections.Generic;
//using System.Reflection;
//using UnityEngine;

//public enum WeaponType { Catapult = 0, Laser, }
//public enum WeaponState { SearchTarget = 0, TryAttackCatapult, TryAttackLaser, }

//public class TowerWeapon : MonoBehaviour
//{
//    [Header("Common")]
//    [SerializeField]
//    private Transform spawnPoint;

//    [SerializeField]
//    private WeaponType weaponType;

//    [SerializeField]
//    private float attackRate = 0.5f;

//    [SerializeField]
//    private float attackRange = 2.0f;

//    [SerializeField]
//    private int attackDamage = 1; // 타워 데미지

//    [SerializeField]
//    private int sellPrice = 25; // 타워 판매 가격

//    [Header("Catapult")]
//    [SerializeField]
//    private GameObject projectilePrefab;

//    [Header("Laser")]
//    [SerializeField]
//    private LineRenderer lineRenderer;

//    [SerializeField]
//    private Transform hitEffect;

//    [SerializeField]
//    private LayerMask targetLayer;

//    private WeaponState weaponState = WeaponState.SearchTarget;
//    private Transform attackTarget = null;
//    private EnemySpawner enemySpawner;
//    private PlayerGold playerGold;
//    private Tile ownerTile;

//    public float Damage => attackDamage;
//    public float Rate => attackRate;
//    public float Range => attackRange;

//    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
//    {
//        this.enemySpawner = enemySpawner;
//        this.playerGold = playerGold;
//        this.ownerTile = ownerTile;

//        ChangeState(WeaponState.SearchTarget);
//    }

//    public void ChangeState(WeaponState newState)
//    {
//        StopCoroutine(weaponState.ToString());

//        weaponState = newState;

//        StartCoroutine(weaponState.ToString());
//    }

//    private void Update()
//    {
//        if (attackTarget != null)
//        {
//            RotateToTarget();
//        }
//    }

//    private void RotateToTarget()
//    {
//        float dx = attackTarget.position.x - transform.position.x;
//        float dy = attackTarget.position.y - transform.position.y;

//        float degree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

//        transform.rotation = Quaternion.Euler(0, 0, degree);
//    }

//    private IEnumerator SearchTarget()
//    {
//        while (true)
//        {
//            attackTarget = FindClosestAttackTarget();

//            if (attackTarget != null)
//            {
//                if (weaponType == WeaponType.Catapult)
//                {
//                    ChangeState(WeaponState.TryAttackCatapult);
//                }
//                else if (weaponType == WeaponType.Laser)
//                {
//                    ChangeState(WeaponState.TryAttackLaser);
//                }
//            }

//            yield return null;
//        }
//    }

//    private Transform FindClosestAttackTarget()
//    {
//        float closestDistSqr = Mathf.Infinity;

//        for (int i = 0; i < enemySpawner.EnemyList.Count; ++i)
//        {
//            float distance = Vector3.Distance(enemySpawner.EnemyList[i].transform.position, transform.position);

//            if (enemySpawner.EnemyList[i].CompareTag("Ground") && distance <= attackRange && distance <= closestDistSqr)
//            {
//                closestDistSqr = distance;
//                attackTarget = enemySpawner.EnemyList[i].transform;
//                Debug.Log("Target found: " + attackTarget.name);  // Debug log
//            }
//        }
//        return attackTarget;
//    }


//    private IEnumerator TryAttackCatapult()
//    {
//        while (true)
//        {
//            if (IsPossibleToAttackTarget() == false)
//            {
//                ChangeState(WeaponState.SearchTarget);
//                break;
//            }

//            SpawnProjectile();

//            yield return new WaitForSeconds(attackRate);
//        }
//    }

//    private IEnumerator TryAttackLaser()
//    {
//        EnableLaser();

//        while (true)
//        {
//            if (IsPossibleToAttackTarget() == false)
//            {
//                DisableLaser();
//                ChangeState(WeaponState.SearchTarget);
//                break;
//            }

//            SpawnLaser();

//            yield return null;
//        }
//    }

//    private void EnableLaser()
//    {
//        lineRenderer.gameObject.SetActive(true);
//        hitEffect.gameObject.SetActive(true);
//    }

//    private void DisableLaser()
//    {
//        lineRenderer.gameObject.SetActive(false);
//        hitEffect.gameObject.SetActive(false);
//    }

//    private void SpawnLaser()
//    {
//        if (attackTarget == null)
//        {
//            DisableLaser();
//            return;
//        }

//        Vector3 direction = attackTarget.position - spawnPoint.position;
//        RaycastHit2D[] hit = Physics2D.RaycastAll(spawnPoint.position, direction, attackRange, targetLayer);

//        for (int i = 0; i < hit.Length; ++i)
//        {
//            if (hit[i].transform == attackTarget)
//            {
//                lineRenderer.SetPosition(0, spawnPoint.position);
//                lineRenderer.SetPosition(1, new Vector3(hit[i].point.x, hit[i].point.y, 0) + Vector3.back);
//                hitEffect.position = hit[i].point;

//                if (attackTarget != null)
//                {
//                    attackTarget.GetComponent<EnemyHP>().TakeDamage(attackDamage * Time.deltaTime);
//                }
//            }
//        }
//    }

//    private bool IsPossibleToAttackTarget()
//    {
//        if (attackTarget == null)
//        {
//            return false;
//        }

//        float distance = Vector3.Distance(attackTarget.position, transform.position);

//        if (distance > attackRange)
//        {
//            attackTarget = null;
//            DisableLaser();
//            ChangeState(WeaponState.SearchTarget);
//            return false;
//        }

//        return true;
//    }

//    private void SpawnProjectile()
//    {
//        //Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

//        GameObject clone = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);

//        clone.GetComponent<Projectile>().Setup(attackTarget, attackDamage);
//    }

//    public void Sell()
//    {
//        playerGold.CurrentGold += sellPrice;
//        ownerTile.IsBuildTower = false;
//        Destroy(gameObject);
//    }
//}
