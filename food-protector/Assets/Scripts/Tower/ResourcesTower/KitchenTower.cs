using System.Collections;
using UnityEngine;

public class KitchenTower : MonoBehaviour, ITower
{
    public float Damage => 0;
    public float Rate => 0;
    public float Range => 0;

    private int sellPrice = 15;

    private PlayerHP playerHP;

    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();

        if (playerHP != null)
        {
            StartCoroutine(GetFood());
        }
        else
        {
            Debug.LogError("PlayerHP 찾을 수 없습니다.");
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
                Debug.Log("5 체력 추가, 현재 체력: " + playerHP.CurrentFood);
            }
            else
            {
                Debug.Log("이번에는 체력을 받지 못했습니다.");
            }
        }
    }

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {

    }

    public void Sell()
    {
        PlayerGold playerGold = FindObjectOfType<PlayerGold>();
        if (playerGold != null)
        {
            playerGold.CurrentGold += sellPrice;
            Debug.Log("Kitchen 타워 판매 완료, 골드: " + playerGold.CurrentGold);
        }

        Destroy(gameObject);
    }
}
