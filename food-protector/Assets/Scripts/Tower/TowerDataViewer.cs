using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerDataViewer : MonoBehaviour
{
    //[SerializeField]
    //private Image imageTower;

    [SerializeField]
    private TextMeshProUGUI textDamage;

    [SerializeField]
    private TextMeshProUGUI textRate;

    [SerializeField]
    private TextMeshProUGUI textRange;

    //[SerializeField]
    //private SystemTextViewer systemTextViewer;

    private TowerWeapon currentTower;

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

    public void OnPanel(Transform towerWeapon)
    {
        currentTower = towerWeapon.GetComponent<TowerWeapon>();

        gameObject.SetActive(true);

        UpdateTowerData();
    }

    public void OffPanel()
    {
        gameObject.SetActive(false);
    }

    private void UpdateTowerData()
    {
        textDamage.text = "Damage: " + currentTower.Damage;
        textRate.text = "Rate: " + currentTower.Rate;
        textRange.text = "Range: " + currentTower.Range;
    }

    public void OnClickEventTowerSell()
    {
        currentTower.Sell();
        OffPanel();
    }
}
