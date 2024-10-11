using System.Collections;
using UnityEngine;

public class LuckyBox : MonoBehaviour, ITower
{
    public float Damage => 0;
    public float Rate => 0;
    public float Range => 0;

    private int sellPrice = 250;
    private PlayerHP playerHP;

    private void Start()
    {
        playerHP = FindObjectOfType<PlayerHP>();

        if (playerHP != null)
        {
            StartCoroutine(GainHealth());
        }
    }

    private IEnumerator GainHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            if (Random.value <= 0.01f)
            {
                playerHP.IncreaseFood(500f);
            }
        }
    }

    public void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile)
    {
    }

    public void Sell()
    {
        PlayerGold playerGold = FindObjectOfType<PlayerGold>();
        if (playerGold != null)
        {
            playerGold.CurrentGold += sellPrice;
        }

        Destroy(gameObject);
    }
}
