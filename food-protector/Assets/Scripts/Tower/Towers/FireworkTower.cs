using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireworkTower : MonoBehaviour, ITower
{
    [SerializeField]
    private float range = 6.0f;

    [SerializeField]
    private float rate = 1.0f;

    [SerializeField]
    private GameObject fireworkPrefab;

    [SerializeField]
    private Transform firePoint;

    [SerializeField]
    private int sellPrice = 90;

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
            attackCooldown = 1f / rate;
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, range);

        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Air"))
            {
                Vector3 direction = enemy.transform.position - firePoint.position;

                GameObject newFirework = Instantiate(fireworkPrefab, firePoint.position, Quaternion.identity);
                Firework fireworkScript = newFirework.GetComponent<Firework>();
                fireworkScript.Setup(enemy.transform, fireworkScript.explosionDamage);

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
