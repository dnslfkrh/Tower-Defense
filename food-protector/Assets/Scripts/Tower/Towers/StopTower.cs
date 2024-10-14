using System.Collections.Generic;
using UnityEngine;

public class StopTower : MonoBehaviour, ITower
{
    [SerializeField]
    private float stopRange = 5.0f;

    [SerializeField]
    private float stopDuration = 1.0f;

    [SerializeField]
    private float interval = 5.0f;

    [SerializeField]
    private int sellPrice = 100;

    private List<Movement2D> stoppedEnemies = new List<Movement2D>();
    private PlayerGold playerGold;
    private EnemySpawner enemySpawner;
    private Tile ownerTile;
    private float stopTimer = 0f;

    public float Damage => 0;
    public float Rate => 0;
    public float Range => stopRange;

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();
    }

    private void Update()
    {
        stopTimer += Time.deltaTime;

        if (stopTimer >= interval)
        {
            StopEnemies();
            stopTimer = 0f;
        }
    }

    private void StopEnemies()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, stopRange);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Air"))
            {
                Movement2D enemyMovement = collider.GetComponent<Movement2D>();
                if (enemyMovement != null && !stoppedEnemies.Contains(enemyMovement))
                {
                    enemyMovement.SetSpeedMultiplier(0);  // 적의 속도를 0으로 설정
                    stoppedEnemies.Add(enemyMovement);
                    StartCoroutine(ResumeEnemyMovementAfterDelay(enemyMovement, stopDuration));
                }
            }
        }
    }

    private System.Collections.IEnumerator ResumeEnemyMovementAfterDelay(Movement2D enemyMovement, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (enemyMovement != null)
        {
            enemyMovement.SetSpeedMultiplier(1.0f);  // 적의 속도를 원래대로 복구
            stoppedEnemies.Remove(enemyMovement);
        }
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
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, stopRange);
    }
}
