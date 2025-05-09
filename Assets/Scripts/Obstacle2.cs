using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 0; // ������ ��ġ
    public float waitPosition = 3f; // ����� ������ ��

    private Transform squareTransform; // Square �ڽ� Transform
    private Vector3 squareInitialPosition; // Square �ʱ� ��ġ

    private bool wait = false; // ������ΰ�?
    private bool returning = false; // �ε巴��

    private void Start()
    {
        // �ڽ� ������Ʈ �� �̸��� "Square"�� ���� ã�Ƽ� Transform ����
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
            //�÷��̾�� ������ �ִ� �����߰� ����

            // if �÷��̾� HP 0�� GameOver �߰� ����?
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