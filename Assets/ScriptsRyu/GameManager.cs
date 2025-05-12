using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverUI gameOverUI; // 게임 오버 UI 스크립트 참조
    private ScoreManager scoreManager; // 점수 관리 스크립트 참조
    private TimeManager timeManager; // 시간 관리 스크립트 참조

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    public void GameOver()
    {
        int finalScore = scoreManager.GetTotalScore(); // 최종 점수 가져오기
        float elapsedTime = timeManager.isRunning ? timeManager.GetElapsedTime() : 0f; // 경과 시간 가져오기

        gameOverUI.ShowGameOverPanel(finalScore, elapsedTime); // 게임 오버 UI 표시
    }
}
