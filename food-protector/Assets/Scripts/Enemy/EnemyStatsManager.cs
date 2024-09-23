using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, float> enemyMaxHPs = new Dictionary<string, float>();

    private Dictionary<string, float> enemyFoodDecreases = new Dictionary<string, float>();

    private void Awake()
    {
        // �� ���� �� "ü��"
        enemyMaxHPs.Add("Seagull", 5f);

        // �� ���� �� "���� �ķ�"
        enemyFoodDecreases.Add("Seagull", 5f);
    }

    public float GetMaxHP(string enemyType)
    {
        if (enemyMaxHPs.TryGetValue(enemyType, out float maxHP))
        {
            return maxHP;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return 0f;
        }
    }

    public float GetDecreaseFoods(string enemyType)
    {
        if (enemyFoodDecreases.TryGetValue(enemyType, out float maxFood))
        {
            return maxFood;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return 0f;
        }
    }
}
