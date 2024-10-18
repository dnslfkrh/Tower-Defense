using System.Collections;
using UnityEngine;

public class KitchenTower : MonoBehaviour, ITower
{
    private int sellPrice = 100;
    private PlayerHP playerHP;
    private PlayerGold playerGold;
    private Tile ownerTile;

    public float Damage => 0;
    public float Rate => 0;
    public float Range => 0;

    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();

        if (playerHP != null)
        {
            StartCoroutine(GetFood());
        }
    }

    private IEnumerator GetFood()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (Random.value <= 0.5f)
            {
                playerHP.IncreaseFood(5f);
            }
        }
    }

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
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
