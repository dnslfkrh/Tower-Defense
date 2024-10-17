using System.Collections;
using UnityEngine;

public enum EnemyDestroyType { kill = 0, Arrive }

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float rotationSpeed = 10f;
    
    private int wayPointCount;

    private Transform[] wayPoints;
    
    private int currentIndex = 0;

    private string enemyType;

    private float dropGold;

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
        // Movement2D ������Ʈ
        movement2D = GetComponent<Movement2D>();

        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ù ��° wayPoint�� �̵�
        transform.position = wayPoints[currentIndex].position;

        // �̵� �ڷ�ƾ ����
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        // wayPoint�� �̵� ����
        NextMoveTo();
        while (true)
        {
            // ���� (��ǥ) wayPoint�� �����ϸ�, ���� wayPoint�� �̵� ����
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            // ��ǥ wayPoint�� �̵� ��

            // �̵� ���� ���
            Vector2 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            // �̵� ���⿡ �´� ���� ���
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // ��ǥ ȸ���� ���
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // ȸ��
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        // ���� �����ؾ� �� waypoint�� ���� ������
        if (currentIndex < wayPointCount - 1)
        {

            transform.position = wayPoints[currentIndex].position;
            
            //���� wayPoint �ε����� ����
            currentIndex++;

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            // movement2D ������Ʈ�� �̵� ���
            movement2D.MoveTo(direction);
        }
        // ��� wayPoint�� �����ٸ� ������Ʈ ����
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