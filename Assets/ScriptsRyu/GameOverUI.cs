using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel; // 게임 오버 패널
    public TextMeshProUGUI finalScoreText; // 최종 점수 텍스트
    public TextMeshProUGUI finalTimeText; // 최종 시간 텍스트

    private void Start()
    {
        gameOverPanel.SetActive(false); // 게임 오버 패널 비활성화
    }

    public void ShowGameOverPanel(int score, float elapsedTime)
    {
        Time.timeScale = 0f;

        // 2) 최종 점수·시간 텍스트 설정
        finalScoreText.text = $"Score: {score}"; // 최종 점수 표시
        int m = Mathf.FloorToInt(elapsedTime / 60f); // 분 계산
        int s = Mathf.FloorToInt(elapsedTime % 60f); // 초 계산
        finalTimeText.text = $"{m:D2}:{s:D2}"; // 최종 시간 표시

        gameOverPanel.SetActive(true);
    }
}
