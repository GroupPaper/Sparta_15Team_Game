using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 10; // 데미지 수치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("트리거 충돌 감지: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌함");
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("데미지 적용됨");
            }
        }
    }
}