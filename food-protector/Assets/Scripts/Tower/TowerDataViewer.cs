using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textDamage;

    [SerializeField]
    private TextMeshProUGUI textRate;

    [SerializeField]
    private TextMeshProUGUI textRange;

    private ITower currentTower; 

    private void Awake()
    {
        OffPanel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OffPanel();
        }
    }

    public void OnPanel(Transform towerTransform)
    {
        currentTower = towerTransform.GetComponent<ITower>();

        if (currentTower == null)
        {
            Debug.LogError("Tower does not implement ITower interface.");
            return;
        }

        gameObject.SetActive(true);

        UpdateTowerData(currentTower);
    }

    private void UpdateTowerData(ITower tower)
    {
        textDamage.text = "Damage: " + tower.Damage;
        textRate.text = "Rate: " + tower.Rate;
        textRange.text = "Range: " + tower.Range;

        if (tower is KitchenTower || tower is MinerTower || tower is LuckyBox)
        {
            textDamage.text = "No Damage";
            textRate.text = "No Attack Rate";
            textRange.text = "No Range";
        }
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    public void OnClickEventTowerSell()
    {
        if (currentTower != null)
        {
            currentTower.Sell();
            OffPanel();
        }
        else
        {
            Debug.LogError("currentTower is null. Cannot sell.");
        }
    }
}
