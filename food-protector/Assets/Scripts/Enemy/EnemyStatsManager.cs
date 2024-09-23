using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, float> enemySpeeds = new Dictionary<string, float>();

    private Dictionary<string, float> enemyMaxHPs = new Dictionary<string, float>();

    private Dictionary<string, float> enemyFoodDecreases = new Dictionary<string, float>();

    private Dictionary<string, float> enemyDropGolds = new Dictionary<string, float>();

    private void Awake()
    {
        AddEnemySpeedList();
        AddEnemyHPList();
        AddEnemyFoodDecreases();
        AddEnemyDropGolds();
    }

    private void AddEnemySpeedList()
    {
        enemySpeeds.Add("Seagull", 1f);
    }

    private void AddEnemyHPList()
    {
        enemyMaxHPs.Add("Seagull", 5f);
    }

    private void AddEnemyFoodDecreases()
    {
        enemyFoodDecreases.Add("Seagull", 5f);
    }

    private void AddEnemyDropGolds()
    {
        enemyDropGolds.Add("Seagull", 10f);
    }

    public float GetSpeed(string enemyType)
    {
        if (enemySpeeds.TryGetValue(enemyType, out float speed))
        {
            return speed;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return 0f;
        }
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
        if (enemyFoodDecreases.TryGetValue(enemyType, out float stolenFood))
        {
            return stolenFood;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return 0f;
        }
    }

    public float GetDropGold(string enemyType)
    {
        if (enemyDropGolds.TryGetValue(enemyType, out float Golds))
        {
            return Golds;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return 0f;
        }
    }
}
