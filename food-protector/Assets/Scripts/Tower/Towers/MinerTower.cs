using System.Collections;
using UnityEngine;

public class MinerTower : MonoBehaviour, ITower
{
    private int sellPrice = 100;
    private PlayerGold playerGold;

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
            Debug.LogError("PlayerGold�� ã�� �� �����ϴ�.");
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
                Debug.Log("100 ��� �߰�, ���� ���: " + playerGold.CurrentGold);
            }
            else
            {
                Debug.Log("�̹����� ��带 ���� ���߽��ϴ�.");
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
        }

        Destroy(gameObject);
    }
}

