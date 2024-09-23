using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, float> enemyMaxHPs = new Dictionary<string, float>();

    private Dictionary<string, float> enemyFoodDecreases = new Dictionary<string, float>();

    private void Awake()
    {
        // 적 유닛 별 "체력"
        enemyMaxHPs.Add("Seagull", 5f);

        // 적 유닛 별 "감소 식량"
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
