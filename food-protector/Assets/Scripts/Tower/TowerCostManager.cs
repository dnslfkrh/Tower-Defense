using System.Collections.Generic;
using UnityEngine;

public class TowerCostManager : MonoBehaviour
{
    private Dictionary<string, int> towerCostDictionary = new Dictionary<string, int>();

    private void Awake()
    {
        AddTowerCosts();
    }

    private void AddTowerCosts()
    {
        towerCostDictionary.Add("Catapult", 50);
        towerCostDictionary.Add("Laser", 50);
        towerCostDictionary.Add("Miner", 200);
        towerCostDictionary.Add("Kitchen", 200);
        towerCostDictionary.Add("Shotgun", 70);
        towerCostDictionary.Add("Bow", 80);
        towerCostDictionary.Add("SlowTower", 150);
        towerCostDictionary.Add("LuckyBox", 500);
        towerCostDictionary.Add("BombTower", 100);
        towerCostDictionary.Add("GasTower", 160);
        towerCostDictionary.Add("FireworkTower", 180);
        towerCostDictionary.Add("StopTower", 200);
    }

    public int GetTowerCost(string towerType)
    {
        if (towerCostDictionary.TryGetValue(towerType, out int cost))
        {
            return cost;
        }
        else
        {
            Debug.LogError("Tower type not found: " + towerType);
            return 0;
        }
    }
}
