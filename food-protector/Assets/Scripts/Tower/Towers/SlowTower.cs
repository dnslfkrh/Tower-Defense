using System.Collections.Generic;
using UnityEngine;

public class SlowTower : MonoBehaviour, ITower
{
    [SerializeField] private float slowRange = 3.0f;
    [SerializeField] private int sellPrice = 75;
    private List<Movement2D> slowedEnemies = new List<Movement2D>();
    private PlayerGold playerGold;

    public float Damage => 0;
    public float Rate => 0; 
    public float Range => slowRange; 

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
    }
    
    private void Update()
    {
        DetectEnemiesInRange();
    }

    private void DetectEnemiesInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slowRange);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Ground") || collider.CompareTag("Air"))
            {
                Movement2D enemyMovement = collider.GetComponent<Movement2D>();
                if (enemyMovement != null && !slowedEnemies.Contains(enemyMovement))
                {
                    enemyMovement.SetSpeedMultiplier(0.5f); 
                    slowedEnemies.Add(enemyMovement);
                }
            }
        }

        for (int i = slowedEnemies.Count - 1; i >= 0; i--)
        {
            Movement2D enemyMovement = slowedEnemies[i];
            if (enemyMovement != null && !IsEnemyInRange(enemyMovement.gameObject))
            {
                enemyMovement.SetSpeedMultiplier(1.0f);
                slowedEnemies.RemoveAt(i);
            }
        }
    }

    private bool IsEnemyInRange(GameObject enemy)
    {
        return Vector3.Distance(transform.position, enemy.transform.position) <= slowRange;
    }

    public void Sell()
    {
        PlayerGold playerGold = FindObjectOfType<PlayerGold>();
        if (playerGold != null)
        {
            playerGold.CurrentGold += sellPrice;
        }

        Destroy(gameObject);
    }
    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
    }
}
