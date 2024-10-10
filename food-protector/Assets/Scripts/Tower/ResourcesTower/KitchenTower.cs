using System.Collections;
using UnityEngine;

public class KitchenTower : MonoBehaviour
{
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
}
