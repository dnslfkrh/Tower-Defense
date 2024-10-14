using System.Collections.Generic;
using UnityEngine;

public class GasTower : MonoBehaviour, ITower
{
    [SerializeField]
    private float attackRange = 3.0f;

    [SerializeField]
    private float damagePerSecond = 1.0f;

    [SerializeField]
    private int sellPrice = 80;

    private List<EnemyHP> enemiesInRange = new List<EnemyHP>();
    private PlayerGold playerGold;
    private EnemySpawner enemySpawner;
    private Tile ownerTile;

    public float Damage => damagePerSecond;
    public float Rate => 0;
    public float Range => attackRange;

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
    }

    private void Update()
    {
        ApplyDamageToEnemies();
        DetectEnemiesInRange();
    }

    private void DetectEnemiesInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Air"))
            {
                EnemyHP enemyHP = collider.GetComponent<EnemyHP>();
                if (enemyHP != null && !enemiesInRange.Contains(enemyHP))
                {
                    enemiesInRange.Add(enemyHP);
                }
            }
        }

        for (int i = enemiesInRange.Count - 1; i >= 0; i--)
        {
            if (enemiesInRange[i] == null || !IsEnemyInRange(enemiesInRange[i].gameObject))
            {
                enemiesInRange.RemoveAt(i);
            }
        }
    }

    private void ApplyDamageToEnemies()
    {
        foreach (EnemyHP enemy in enemiesInRange)
        {
            if (enemy != null)
            {
                enemy.TakeDamage(damagePerSecond * Time.deltaTime);
            }
        }
    }

    private bool IsEnemyInRange(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) <= attackRange;
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
