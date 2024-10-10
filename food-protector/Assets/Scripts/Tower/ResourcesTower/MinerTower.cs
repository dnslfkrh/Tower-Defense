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
}

