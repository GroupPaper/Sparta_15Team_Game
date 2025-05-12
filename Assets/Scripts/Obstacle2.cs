using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public float damage = 10f; // 데미지 수치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("충돌한 오브젝트: " + collision.gameObject.name);  // 충돌한 객체 이름 확인
        Debug.Log("충돌한 객체의 태그: " + collision.gameObject.tag);  // 충돌한 객체의 태그 확인

        // Player 태그를 가진 객체가 충돌했을 때
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("플레이어와 충돌함");

            // 부모 오브젝트에서 Player 컴포넌트 가져오기
            Player player = collision.transform.parent.GetComponent<Player>();

            if (player != null)
            {
                player.TakeDamage(damage);
                Debug.Log("데미지 적용됨" + damage);
            }
            else
            {
                Debug.LogWarning("Player 컴포넌트를 찾지 못했습니다!");
            }
        }
        else
        {
            Debug.LogWarning("플레이어가 아닌 오브젝트와 충돌했습니다.");
        }
    }
}