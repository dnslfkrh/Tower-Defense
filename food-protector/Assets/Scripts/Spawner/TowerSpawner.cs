using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] towerPrefab;

    [SerializeField]
    private EnemySpawner enemySpawner;

    [SerializeField]
    private PlayerGold playerGold;

    [SerializeField]
    private SystemTextViewer systemTextViewer;

    [SerializeField]
    private TowerCostManager towerCostManager;

    private int towerBuildGold;
    private bool isOnTowerButton = false;
    private string towerType;

    private Dictionary<string, int> towerTypeIndex = new Dictionary<string, int>()
    {
        { "Catapult", 0 },
        { "Laser", 1 },
    };

    public void ReadyToSpawnTower(string type)
    {
        towerType = type;

        towerBuildGold = towerCostManager.GetTowerCost(type);

        if (isOnTowerButton)
        {
            return;
        }

        if (towerBuildGold > playerGold.CurrentGold)
        {
            systemTextViewer.PrintText(SystemType.Money);
            return;
        }

        isOnTowerButton = true;
    }

    public void SpawnTower(Transform tileTransform)
    {
        if (!isOnTowerButton)
        {
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower)
        {
            systemTextViewer.PrintText(SystemType.Build);
            return;
        }

        isOnTowerButton = false;

        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerBuildGold;

        if (towerTypeIndex.TryGetValue(towerType, out int towerIndex))
        {
            Vector3 position = tileTransform.position + Vector3.back;
            GameObject clone = Instantiate(towerPrefab[towerIndex], position, Quaternion.identity);
            clone.GetComponent<TowerWeapon>().Setup(enemySpawner, playerGold, tile);
        }
        else
        {
            Debug.LogError("Invalid tower type: " + towerType);
        }
    }
}
