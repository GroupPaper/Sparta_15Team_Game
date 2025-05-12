using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle2 : MonoBehaviour
{
    public int damage = 0; // 데미지 수치

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"트리거와 접촉하여 {damage}의 데미지를 입혔습니다.");
            //플레이어에게 데미지 주는 내용추가 예정

            // if 플레이어 HP 0씨 GameOver 뜨게 예정?
        }
    }

/*    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log($"콜라이더와 충돌하여 {damage}의 데미지를 입혔습니다.");
            //위와 같음
        }
    }*/
}