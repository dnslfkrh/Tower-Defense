using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystem : MonoBehaviour
{
    [SerializeField]
    private Wave[] waves;

    [SerializeField]
    private EnemySpawner enemySpawner;

    private int currentWaveIndex = -1;

    public int CurrentWave => currentWaveIndex + 1;
    public int MaxWave => waves.Length;

    public void StartWave()
    {
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }

    private void Update()
    {
        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Length)
        {
            if (enemySpawner.EnemyList.Count == 0 && !enemySpawner.IsWaveActive)
            {
                if (currentWaveIndex >= MaxWave - 1)
                {
                    GameManager.Instance.GameClear();
                }
                else
                {
                    StartWave();
                }
            }
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;
    public int maxEnemyCount;
    public GameObject[] enemyPrefabs;
}
