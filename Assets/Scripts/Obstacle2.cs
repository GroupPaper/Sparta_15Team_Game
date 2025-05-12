using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public float damage = 10f; // ������ ��ġ

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹�� ������Ʈ: " + collision.gameObject.name);  // �浹�� ��ü �̸� Ȯ��
        Debug.Log("�浹�� ��ü�� �±�: " + collision.gameObject.tag);  // �浹�� ��ü�� �±� Ȯ��

        // Player �±׸� ���� ��ü�� �浹���� ��
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�÷��̾�� �浹��");

            // �θ� ������Ʈ���� Player ������Ʈ ��������
            Player player = collision.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("������ �����" + damage);
            }
            else
            {
                Debug.LogWarning("Player ������Ʈ�� ã�� ���߽��ϴ�!");
            }
        }
        else
        {
            Debug.LogWarning("�÷��̾ �ƴ� ������Ʈ�� �浹�߽��ϴ�.");
        }
    }
}