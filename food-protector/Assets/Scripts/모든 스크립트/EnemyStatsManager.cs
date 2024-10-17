using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    public float Speed { get; private set; }
    public float MaxHP { get; private set; }
    public float FoodDecrease { get; private set; }
    public float DropGold { get; private set; }

    public EnemyStats(float speed, float maxHP, float foodDecrease, float dropGold)
    {
        Speed = speed;
        MaxHP = maxHP;
        FoodDecrease = foodDecrease;
        DropGold = dropGold;
    }
}

public class EnemyStatsManager : MonoBehaviour
{
    private Dictionary<string, EnemyStats> enemyStatsDictionary = new Dictionary<string, EnemyStats>();

    private void Awake()
    {
        AddEnemyStats();
    }

    private void AddEnemyStats()
    {
        enemyStatsDictionary.Add("Crab", new EnemyStats(0.5f, 8f, 4f, 8f));
        enemyStatsDictionary.Add("Camel", new EnemyStats(2.0f, 15f, 6f, 18f));
        enemyStatsDictionary.Add("Wolf", new EnemyStats(4.5f, 12f, 8f, 15f));
        enemyStatsDictionary.Add("Pig", new EnemyStats(1.5f, 10f, 5f, 12f));
        enemyStatsDictionary.Add("SeaTurtle", new EnemyStats(0.8f, 18f, 3f, 20f));
        enemyStatsDictionary.Add("Snake", new EnemyStats(2.2f, 6f, 7f, 10f));
        enemyStatsDictionary.Add("PolarBear", new EnemyStats(3.0f, 20f, 12f, 25f));
        enemyStatsDictionary.Add("Reindeer", new EnemyStats(4.0f, 14f, 5f, 16f));
        enemyStatsDictionary.Add("DesertFox", new EnemyStats(4.2f, 7f, 4f, 9f));
        enemyStatsDictionary.Add("Cow", new EnemyStats(1.8f, 16f, 7f, 20f));
        enemyStatsDictionary.Add("Monkey", new EnemyStats(3.5f, 8f, 5f, 11f));
        enemyStatsDictionary.Add("Fox", new EnemyStats(4.0f, 9f, 6f, 12f));
        enemyStatsDictionary.Add("Seagull", new EnemyStats(3.8f, 5f, 5f, 10f));
        enemyStatsDictionary.Add("Crow", new EnemyStats(3.0f, 4f, 4f, 8f));
        enemyStatsDictionary.Add("Eagle", new EnemyStats(4.7f, 7f, 8f, 15f));
        enemyStatsDictionary.Add("Owl", new EnemyStats(2.5f, 6f, 7f, 12f));
        enemyStatsDictionary.Add("Parrot", new EnemyStats(3.0f, 4f, 3f, 7f));
        enemyStatsDictionary.Add("Swallow", new EnemyStats(4.9f, 3f, 2f, 5f));
        enemyStatsDictionary.Add("Sparrow", new EnemyStats(4.0f, 2f, 1f, 3f));
        enemyStatsDictionary.Add("WhiteHawk", new EnemyStats(5.0f, 8f, 9f, 18f));
    }

    public EnemyStats GetEnemyStats(string enemyType)
    {
        if (enemyStatsDictionary.TryGetValue(enemyType, out EnemyStats stats))
        {
            return stats;
        }
        else
        {
            Debug.LogError("Enemy type not found: " + enemyType);
            return null;
        }
    }

    public float GetSpeed(string enemyType)
    {
        EnemyStats stats = GetEnemyStats(enemyType);
        return stats != null ? stats.Speed : 0f;
    }

    public float GetMaxHP(string enemyType)
    {
        EnemyStats stats = GetEnemyStats(enemyType);
        return stats != null ? stats.MaxHP : 0f;
    }

    public float GetDecreaseFood(string enemyType)
    {
        EnemyStats stats = GetEnemyStats(enemyType);
        return stats != null ? stats.FoodDecrease : 0f;
    }

    public float GetDropGold(string enemyType)
    {
        EnemyStats stats = GetEnemyStats(enemyType);
        return stats != null ? stats.DropGold : 0f;
    }
}
