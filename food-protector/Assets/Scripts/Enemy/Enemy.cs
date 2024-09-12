using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int wayPointCount;
    private Transform[] wayPoints;
    private int currentIndex = 0;
    private Movement2D movement2D;

    [SerializeField]
    private float rotationSpeed = 5f;

    public void Setup(Transform[] wayPoints)
    {
        // Movement2D ������Ʈ
        movement2D = GetComponent<Movement2D>();
        
        // �� wayPoint ����
        wayPointCount = wayPoints.Length; 
        
        // wayPoint �迭 ����
        this.wayPoints = new Transform[wayPointCount];
        this.wayPoints = wayPoints;

        // ù ��° wayPoint�� �̵�
        transform.position = wayPoints[currentIndex].position;
        
        // �̵� �ڷ�ƾ ����
        StartCoroutine("OnMove");
    }

    public IEnumerator OnMove()
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
            //���� wayPoint �ε����� ����
            currentIndex++;

            Vector3 direction = (wayPoints[currentIndex].position - transform.position).normalized;
            
            // movement2D ������Ʈ�� �̵� ���
            movement2D.MoveTo(direction);
        }
        // ��� wayPoint�� �����ٸ� ������Ʈ ����
        else
        {
            Destroy(gameObject);
        }
    }
}