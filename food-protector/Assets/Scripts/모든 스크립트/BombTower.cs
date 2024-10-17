using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombTower : MonoBehaviour, ITower
{
    [SerializeField] 
    private float range = 5.0f;
    
    [SerializeField]
    private float rate = 0.2f;

    [SerializeField]
    private GameObject bombPrefab;
    
    [SerializeField]
    private Transform firePoint;
    
    [SerializeField]
    private int sellPrice = 50;

    private float attackCooldown = 0f;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;

    public float Damage => 0;
    public float Rate => rate;
    public float Range => range;

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
    }

    private void Update()
    {
        attackCooldown -= Time.deltaTime;

        if (attackCooldown <= 0f)
        {
            Attack();
            attackCooldown = 5f;
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);

        if (hitEnemies.Length == 0)
        {
            Debug.Log("범위 안에 적이 없습니다.");
        }

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Ground"))
            {
                Vector3 direction = enemy.transform.position - firePoint.position;

                GameObject newBomb = Instantiate(bombPrefab, firePoint.position, Quaternion.identity);
                Bomb bombScript = newBomb.GetComponent<Bomb>();
                bombScript.Setup(enemy.transform, bombScript.explosionDamage);

                Debug.Log("폭탄 발사");
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    public void Sell()
    {
        playerGold.CurrentGold += sellPrice;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
    }
}
