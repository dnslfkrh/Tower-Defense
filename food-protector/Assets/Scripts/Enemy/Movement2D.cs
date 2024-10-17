using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float baseMoveSpeed;

    private Vector3 moveDirection = Vector3.zero;
    private float currentSpeedMultiplier = 1.0f;

    public float MoveSpeed => baseMoveSpeed * currentSpeedMultiplier;

    private void Start()
    {
        EnemyStatsManager enemyStatsManager = FindObjectOfType<EnemyStatsManager>();
        string enemyType = GetComponent<EnemyHP>().enemyType;

        baseMoveSpeed = enemyStatsManager.GetSpeed(enemyType);
    }

    private void Update()
    {
        transform.position += moveDirection * MoveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirection = direction;
    }

    public void SetSpeedMultiplier(float multiplier)
    {
        currentSpeedMultiplier = multiplier;
    }
}