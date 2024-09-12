using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 0.0f;

    [SerializeField]
    private Vector3 moveDirectiron = Vector3.zero;
    
    public float MoveSpeed => moveSpeed;

    private void Update()
    {
        transform.position += moveDirectiron * moveSpeed * Time.deltaTime;
    }

    public void MoveTo(Vector3 direction)
    {
        moveDirectiron = direction;
    }
}