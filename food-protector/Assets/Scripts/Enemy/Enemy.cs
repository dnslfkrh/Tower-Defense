using System.Collections;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive }

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;

    private int waypointCount;
    private int currentIndex = 0;
    private string enemyType;
    private float dropGold;
    private Transform[] wayPoints;
    private Movement2D movement2D;
    private EnemySpawner enemySpawner;
    private EnemyStatsManager enemyStatsManager;

    private void Start()
    {
        enemyStatsManager = FindObjectOfType<EnemyStatsManager>();
        enemyType = GetComponent<EnemyHP>().enemyType;
        dropGold = enemyStatsManager.GetDropGold(enemyType);
    }

    public void Setup(EnemySpawner enemySpawner, Transform[] wayPoints)
    {
        movement2D = GetComponent<Movement2D>();

        this.enemySpawner = enemySpawner;
        waypointCount = wayPoints.Length;

        this.wayPoints = new Transform[waypointCount];
        this.wayPoints = wayPoints;

        transform.position = wayPoints[currentIndex].position;

        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        NextMoveTo();
        while (true)
        {
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            Vector2 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        if (currentIndex < waypointCount - 1)
        {

            transform.position = wayPoints[currentIndex].position;

            currentIndex++;

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            movement2D.MoveTo(direction);
        }
        else
        {
            dropGold = 0;

            OnDie(EnemyDestroyType.Arrive);
        }
    }

    public void OnDie(EnemyDestroyType type)
    {
        enemySpawner.DestroyEnemy(type, this, dropGold);
    }
}
