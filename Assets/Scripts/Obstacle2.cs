using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 0; // ������ ��ġ

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //�÷��̾�� ������ �ִ� �����߰� ����

            // if �÷��̾� HP 0�� GameOver �߰� ����?
        }
    }
}