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
    private int towerBuiltdGold = 50; // Ÿ�� �Ǽ� ���

    [SerializeField]
    private PlayerGold playerGold;


    public void SpawnTower(Transform tileTransform)
    {
        if (towerBuiltdGold > playerGold.CurrentGold)
        {
            Debug.Log("���� �����մϴ�." + playerGold.CurrentGold);
            return;
        }

        Tile tile = tileTransform.GetComponent<Tile>();

        if (tile.IsBuildTower == true)
        {
            return;
        }

        tile.IsBuildTower = true;

        playerGold.CurrentGold -= towerBuiltdGold;

        GameObject clone = Instantiate(towerPrefab, tileTransform.position, Quaternion.identity);

        clone.GetComponent<TowerWeapon>().Setup(enemySpawner);
    }
}
