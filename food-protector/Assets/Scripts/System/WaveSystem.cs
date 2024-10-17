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
        // 웨이브가 시작할 수 있는지 확인
        if (enemySpawner.EnemyList.Count == 0 && currentWaveIndex < waves.Length - 1)
        {
            currentWaveIndex++;
            enemySpawner.StartWave(waves[currentWaveIndex]);
        }
    }

    private void Update()
    {
        // 현재 웨이브의 적이 모두 제거되었는지 확인
        if (currentWaveIndex >= 0 && currentWaveIndex < waves.Length)
        {
            if (enemySpawner.EnemyList.Count == 0 && !enemySpawner.IsWaveActive)
            {
                // 모든 웨이브 클리어 여부 확인
                if (currentWaveIndex >= MaxWave - 1)
                {
                    // 마지막 웨이브가 클리어되면 게임 클리어 호출
                    GameManager.Instance.GameClear();
                }
                else
                {
                    // 다음 웨이브 시작
                    StartWave();
                }
            }
        }
    }
}

[System.Serializable]
public struct Wave
{
    public float spawnTime;          // 적 생성 간격
    public int maxEnemyCount;        // 최대 적 수
    public GameObject[] enemyPrefabs; // 적 프리팹 배열
}
