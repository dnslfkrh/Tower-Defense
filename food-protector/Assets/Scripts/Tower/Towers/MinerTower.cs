using System.Collections;
using UnityEngine;

public class MinerTower : MonoBehaviour, ITower
{
    private int sellPrice = 100;
    private PlayerGold playerGold;
    private Tile ownerTile;

    public float Damage => 0;
    public float Rate => 0;
    public float Range => 0;

    private void Start()
    {
        playerGold = FindObjectOfType<PlayerGold>();

        if (playerGold != null)
        {
            StartCoroutine(GetMoney());
        }
        else
        {
            Debug.LogError("PlayerGold를 찾을 수 없습니다.");
        }
    }

    private IEnumerator GetMoney()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            if (Random.value <= 0.5f)
            {
                playerGold.CurrentGold += 100f;
                Debug.Log("100 골드 추가, 현재 골드: " + playerGold.CurrentGold);
            }
            else
            {
                Debug.Log("이번에는 골드를 받지 못했습니다.");
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

