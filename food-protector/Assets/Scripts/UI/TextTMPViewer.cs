using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextTMPViewer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textPlayerHP;

    [SerializeField]
    private PlayerHP playerHP;

    private void Update()
    {
        // "남은 음식 / 시작 음식"
        textPlayerHP.text = playerHP.CurrentFood + "/" + playerHP.MaxFood;
    }
}
