using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;

    [SerializeField]
    private TextMeshProUGUI textPlayerGold;

    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private PlayerGold playerGold;

    private void Update()
    {
        // "���� ���� / ���� ����"
        textPlayerHP.text = playerHP.CurrentFood + "/" + playerHP.MaxFood;

        // ���� ���
        textPlayerGold.text = playerGold.CurrentGold.ToString();
    }
}
