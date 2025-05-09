using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player_Test : MonoBehaviour
{
    public Player_Test player;
    public int healValue; // 회복 아이템 수치
    public HPBar maxHP; // 최대 체력
    public HPBar currentHP; // 현재 체력
    public int bonusScore; // 추가 점수

    public float baseSpeed = 5f; // 기본 속도
    public float currentSpeed; // 현재 속도
    public int currentScore; // 현재 점수

    // 1. 점수 상승 아이템 - ScoreManager의 ItemScore 상승시키기
    // 2. 체력 회복 아이템 - HPBar에서 Heal함수로 구현함함
    // 3. 속도 상승 아이템 - Player의 acceleration 수치 조절하기

    // 속도 증가 아이템
    public void ApplySpeedBooster(float bonusSpeed, float duration)
    {
        StopCoroutine("ResetSpeedAfter");
        float ItemSpeed = currentSpeed * bonusSpeed; // 코루틴으로 일정시간 지연시키기기 3초동안 이동 속도 증가

        StartCoroutine(ResetSpeedAfter(duration));
    }

    private IEnumerator ResetSpeedAfter(float duration)
    {
        yield return new WaitForSeconds(duration);
    }

    // 점수 증가 아이템
    public void AddSocre(int bonusScore)
    {
        currentScore += bonusScore;
    }
}
