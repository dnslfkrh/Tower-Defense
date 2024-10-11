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
            Debug.LogError("PlayerHP ã�� �� �����ϴ�.");
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
                Debug.Log("5 ü�� �߰�, ���� ü��: " + playerHP.CurrentFood);
            }
            else
            {
                Debug.Log("�̹����� ü���� ���� ���߽��ϴ�.");
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
            Debug.Log("Kitchen Ÿ�� �Ǹ� �Ϸ�, ���: " + playerGold.CurrentGold);
        }

        Destroy(gameObject);
    }
}
