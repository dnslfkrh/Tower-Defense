using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private Vector3 moveDirectiron = Vector3.zero;

    private EnemyStatsManager enemyStatsManager;

    private string enemyType;

    public float MoveSpeed => moveSpeed;

    private void Start()
    {
        enemyStatsManager = FindObjectOfType<EnemyStatsManager>();

        enemyType = GetComponent<EnemyHP>().enemyType;

        moveSpeed = enemyStatsManager.GetSpeed(enemyType);
    }

    private void Update()
    {
        transform.position += moveDirectiron * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirectiron = direction;
    }
}
