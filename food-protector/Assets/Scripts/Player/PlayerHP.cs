using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHP : MonoBehaviour
{
    [SerializeField]
    private Image screenImage;

    [SerializeField]
    private float maxFood;

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

        StopCoroutine("HitAlphaAnimation");
        StartCoroutine("HitAlphaAnimation");

        if (currentFood <= 0)
        {

        }
    }

    private IEnumerator HitAlphaAnimation()
    {
        Color color = screenImage.color;
        color.a = 0.4f;
        screenImage.color = color;

        while (color.a >= 0.0f)
        {
            color.a -= Time.deltaTime;
            screenImage.color = color;

            yield return null;
        }
    }
}
