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
        // Movement2D 컴포넌트
        movement2D = GetComponent<Movement2D>();

        this.enemySpawner = enemySpawner;

        wayPointCount = wayPoints.Length;
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // 첫 번째 wayPoint로 이동
        transform.position = wayPoints[currentIndex].position;

        // 이동 코루틴 시작
        StartCoroutine("OnMove");
    }

    private IEnumerator OnMove()
    {
        // wayPoint로 이동 시작
        NextMoveTo();
        while (true)
        {
            // 현재 (목표) wayPoint에 도착하면, 다음 wayPoint로 이동 시작
            if (Vector3.Distance(transform.position, wayPoints[currentIndex].position) < 0.02f * movement2D.MoveSpeed)
            {
                NextMoveTo();
            }

            // 목표 wayPoint로 이동 중

            // 이동 방향 계산
            Vector2 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            // 이동 방향에 맞는 각도 계산
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            // 목표 회전값 계산
            Quaternion targetRotation = Quaternion.Euler(0, 0, angle);

            // 회전
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            yield return null;
        }
    }

    private void NextMoveTo()
    {
        // 아직 도달해야 할 waypoint가 남아 있으면
        if (currentIndex < wayPointCount - 1)
        {

            transform.position = wayPoints[currentIndex].position;
            
            //다음 wayPoint 인덱스로 변경
            currentIndex++;

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;

            // movement2D 컴포넌트로 이동 명령
            movement2D.MoveTo(direction);
        }
        // 모든 wayPoint를 지났다면 오브젝트 삭제
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