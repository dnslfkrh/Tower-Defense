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
        textPlayerHP.text = playerHP.CurrentFood + "";

        textPlayerGold.text = playerGold.CurrentGold.ToString();

        textWave.text = waveSystem.CurrentWave + "/" + waveSystem.MaxWave;
    }
}
