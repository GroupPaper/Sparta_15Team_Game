using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 0; // 데미지 수치
    public float waitPosition = 3f; // 대기할 포지션 값

    private Transform squareTransform; // Square 자식 Transform
    private Vector3 squareInitialPosition; // Square 초기 위치

    private bool wait = false; // 대기중인가?
    private bool returning = false; // 부드럽게

    private void Start()
    {
        // 자식 오브젝트 중 이름이 "Square"인 것을 찾아서 Transform 저장
        squareTransform = transform.Find("Square");

        if (squareTransform != null)
        {
            squareInitialPosition = squareTransform.position;
        }
    }

    private void Update()
    {
        if (squareTransform == null) return;

        if (!wait && squareTransform.position.y < waitPosition)
        {
            wait = true;
        }

        if (returning)
        {
            squareTransform.position = Vector3.MoveTowards(squareTransform.position, squareInitialPosition, Time.deltaTime * 5f);

            if (Vector3.Distance(squareTransform.position, squareInitialPosition) < 0.01f)
            {
                squareTransform.position = squareInitialPosition;
                returning = false;
                wait = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어에게 데미지 주는 내용추가 예정

            // if 플레이어 HP 0씨 GameOver 뜨게 예정?
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (wait && collision.gameObject.CompareTag("Player"))
        {
            returning = true;
        }
    }
}