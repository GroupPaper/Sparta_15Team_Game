using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // UI 텍스트 컴포넌트
    private float elapsedTime = 0f; // 경과 시간
    public bool isRunning = true; // 타이머 실행 여부

    private void Update()
    {
        if(!isRunning) return; // 타이머가 실행 중이 아닐 경우 업데이트 하지 않음

        elapsedTime += Time.deltaTime; // 경과 시간 증가
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // 분 계산
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // 초 계산
        timeText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds); // UI 텍스트 업데이트
    }

    public float GetElapsedTime() // 경과 시간 반환
    {
        return elapsedTime; // 경과 시간 반환
    }
}
