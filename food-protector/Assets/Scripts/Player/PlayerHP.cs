using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private float maxFood = 20;
    private float currentFood;

    public float MaxFood => maxFood;
    public float CurrentFood => currentFood;

    private void Awake()
    {
        currentFood = maxFood;
    }

    public void DecreaseFood(float stolenFood)
    {
        currentFood -= stolenFood;

        if (currentFood <= 0)
        {

        }
    }
}
