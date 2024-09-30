using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject towerPrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private int towerBuiltdGold = 50; // 타워 건설 비용

    [SerializeField]
    private PlayerGold playerGold;

    [SerializeField]
    private SystemTextViewer systemTextViewer;

    private bool isOnTowerButton = false;

    public void ReadyToSpawnTower()
    {
        if (isOnTowerButton == true)
        {
            return;
        }

        if (towerBuiltdGold > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        isOnTowerButton = true;

        //StartCoroutine("OnTowerCancelSystem");
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (isOnTowerButton == false)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        isOnTowerButton = false;

        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerBuiltdGold;

        Vector3 position = tileTransform.position + Vector3.back;
        GameObject clone = Instantiate(towerPrefab, position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);

        //StopCoroutine("OnTowerCancelSystem");
    }
    //private IEnumerator OnTowerCancelSystem()
    //{
    //    while (true)
    //    {
    //        if (Input.GetKeyDown(KeyCode.Escape))
    //        {
    //            isOnTowerButton = false;
    //            break;
    //        }
    //    }

    //    yield return null;
    //}
}
