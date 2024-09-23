using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, float> enemyMaxHPs = new Dictionary<string, float>();

    private void Awake()
    {
        // 적 유닛 별 (일단)체력(만)
        enemyMaxHPs.Add("Seagull", 5f);

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
}
