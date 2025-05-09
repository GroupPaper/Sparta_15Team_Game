using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI 텍스트 컴포넌트
    public TextMeshProUGUI bestScoreText; // UI 텍스트 컴포넌트

    public float scoreMultiplier = 1f; // 초당 점수 증가율

    private int timeScore = 0; // 점수
    private int itemScore = 0; // 아이템 점수
    private float timeAccumulator = 0; // 누적 시간

    private int bestScore = 0; // 최고 점수


    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0); // 저장된 최고 점수 불러오기
        UpdateBestScoreText(); // UI 텍스트 업데이트

        UpdateScoreText(); // UI 텍스트 업데이트
    }
    private void Update()
    {
        timeAccumulator += Time.deltaTime; // 누적 시간 증가

        if (timeAccumulator >= 1f) // 1초마다 점수 증가
        {
            int gained = Mathf.FloorToInt(timeAccumulator * scoreMultiplier); // 점수 계산
            timeScore += gained; // 점수 증가
            timeAccumulator = 0; // 누적 시간 초기화
            UpdateScoreText(); // UI 텍스트 업데이트
        }
    }

    public int GetTotalScore() // 총 점수 반환
    {
        return timeScore + itemScore; // 시간 점수와 아이템 점수 합산
    }

    private void UpdateScoreText()
    {
        int total = timeScore + itemScore; // 총 점수 계산
        Debug.Log("Total Score: " + total); // 디버그 로그 출력
        scoreText.text = "Score: " + total.ToString(); // UI 텍스트 업데이트

        if (total > bestScore) // 최고 점수 갱신
        {
            bestScore = total; // 최고 점수 갱신
            PlayerPrefs.SetInt("BestScore", bestScore); // 저장
            UpdateBestScoreText(); // UI 텍스트 업데이트
        }
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString(); // UI 텍스트 업데이트
    }
}