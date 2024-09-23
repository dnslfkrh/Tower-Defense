using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, float> enemyMaxHPs = new Dictionary<string, float>();

    private void Awake()
    {
        // �� ���� �� (�ϴ�)ü��(��)
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
