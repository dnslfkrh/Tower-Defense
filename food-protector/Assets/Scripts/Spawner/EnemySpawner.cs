using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyHPSliderPrefab;

    [SerializeField]
    private Transform canvasTransform;

    [SerializeField]
    private Transform[] waypoints;

    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private EnemyStatsManager enemyStatsManager;

    [SerializeField]
    private PlayerGold playerGold;
    
    private bool isWaveActive = false;
    private List<Enemy> enemyList;
    private Wave currentWave;

    public List<Enemy> EnemyList => enemyList;
    public bool IsWaveActive => isWaveActive;

    private void Awake()
    {
        enemyList = new List<Enemy>();
    }

    public void StartWave(Wave wave)
    {
        currentWave = wave;
        isWaveActive = true;
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy()
    {
        int spawnEnemyCount = 0;

        while (spawnEnemyCount < currentWave.maxEnemyCount)
        {
            int enemyIndex = Random.Range(0, currentWave.enemyPrefabs.Length);
            GameObject clone = Instantiate(currentWave.enemyPrefabs[enemyIndex]);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, waypoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            spawnEnemyCount++;
            yield return new WaitForSeconds(currentWave.spawnTime);
        }

        isWaveActive = false;
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, float dropGold)
    {
        if (type == EnemyDestroyType.Arrive)
        {
            EnemyHP enemyHP = enemy.GetComponent<EnemyHP>();
            float decreaseAmount = enemyStatsManager.GetDecreaseFood(enemyHP.enemyType);
            playerHP.DecreaseFood(decreaseAmount);
        }
        else if (type == EnemyDestroyType.kill)
        {
            playerGold.CurrentGold += dropGold;
        }

        enemyList.Remove(enemy);
        Destroy(enemy.gameObject);
    }

    private void SpawnEnemyHPSlider(GameObject enemy)
    {
        GameObject sliderClone = Instantiate(enemyHPSliderPrefab);

        sliderClone.transform.SetParent(canvasTransform);
        sliderClone.transform.localScale = Vector3.one;

        sliderClone.GetComponent<SliderPositionAutoSetter>().Setup(enemy.transform);
        sliderClone.GetComponent<EnemyHPViewer>().Setup(enemy.GetComponent<EnemyHP>());
    }
}
