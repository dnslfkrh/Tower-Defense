using System.Collections;
using UnityEngine;

public class LuckyBox : MonoBehaviour, ITower
{
    public float Damage => 0;
    public float Rate => 0;
    public float Range => 0;

    private int sellPrice = 250;
    private PlayerHP playerHP;
    private EnemySpawner enemySpawner;
    private PlayerGold playerGold;
    private Tile ownerTile;

    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();

        if (playerHP != null)
        {
            StartCoroutine(GainHealth());
        }
    }

    private IEnumerator GainHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (Random.value <= 0.01f)
            {
                playerHP.IncreaseFood(500f);
            }
        }
    }

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
        this.enemySpawner = enemySpawner;
        this.playerGold = playerGold;
        this.ownerTile = ownerTile;
    }

    public void Sell()
    {
        playerGold.CurrentGold += sellPrice;
        ownerTile.IsBuildTower = false;
        Destroy(gameObject);
    }
}
