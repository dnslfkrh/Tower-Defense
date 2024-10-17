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
    private TextMeshProUGUI textWave;

    [SerializeField]
    private PlayerHP playerHP;

    [SerializeField]
    private PlayerGold playerGold;

    [SerializeField]
    private WaveSystem waveSystem;

    private void Update()
    {
        // "남은 음식 / 시작 음식"
        textPlayerHP.text = playerHP.CurrentFood + "";

        // 가진 골드
        textPlayerGold.text = playerGold.CurrentGold.ToString();

        // 웨이브 정보
        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
