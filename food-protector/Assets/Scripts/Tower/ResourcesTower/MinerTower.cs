using System.Collections;
using UnityEngine;

public class MinerTower : MonoBehaviour
{
    private PlayerGold playerGold;

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
}

