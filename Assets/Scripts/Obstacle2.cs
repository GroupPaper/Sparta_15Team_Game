using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 0; // ������ ��ġ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"Ʈ���ſ� �����Ͽ� {damage}�� �������� �������ϴ�.");
            //�÷��̾�� ������ �ִ� �����߰� ����

            // if �÷��̾� HP 0�� GameOver �߰� ����?
        }
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"�ݶ��̴��� �浹�Ͽ� {damage}�� �������� �������ϴ�.");
            //���� ����
        }
    }*/
}