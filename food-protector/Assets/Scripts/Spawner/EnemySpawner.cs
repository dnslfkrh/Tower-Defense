using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnTime;

    [SerializeField]
    private GameObject enemyHPSliderPrefab;

    [SerializeField]
    private Transform canvasTransform;

    [SerializeField]
    private Transform[] wayPoints;

    [SerializeField]
    private PlayerHP playerHP;

    private List<Enemy> enemyList;

    [SerializeField]
    private EnemyStatsManager enemyStatsManager;

    [SerializeField]
    private PlayerGold playerGold;

    public List<Enemy> EnemyList => enemyList;

    private void Awake()
    {
        enemyList = new List<Enemy>();

        StartCoroutine("SpawnEnemy");
    }

    private IEnumerator SpawnEnemy()
    {
        while (true)
        {
            GameObject clone = Instantiate(enemyPrefab);
            Enemy enemy = clone.GetComponent<Enemy>();

            enemy.Setup(this, wayPoints);
            enemyList.Add(enemy);

            SpawnEnemyHPSlider(clone);

            yield return new WaitForSeconds(spawnTime);
        }
    }

    public void DestroyEnemy(EnemyDestroyType type, Enemy enemy, float dropGold)
    {
        // 적이 목표 지점에 도착하면 음식 감소
        if (type == EnemyDestroyType.Arrive)
        {
            EnemyHP enemyHP = enemy.GetComponent<EnemyHP>();

            float decreaseAmount = enemyStatsManager.GetDecreaseFoods(enemyHP.enemyType);

            playerHP.DecreaseFood(decreaseAmount);
        }
        else if (type ==EnemyDestroyType.kill)
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
