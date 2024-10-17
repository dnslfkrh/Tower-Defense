using UnityEngine;

public interface ITower
{
    float Damage { get; }
    float Rate { get; }
    float Range { get; }
    void Setup(EnemySpawner enemySpawner, PlayerGold playerGold, Tile ownerTile);
    void Sell();
}
