using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 10; // ������ ��ġ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Ʈ���� �浹 ����: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹��");
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("������ �����");
            }
        }
    }
}