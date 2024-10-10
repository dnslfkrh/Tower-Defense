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
}
