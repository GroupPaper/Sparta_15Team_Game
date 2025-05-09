using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverUI gameOverUI; // ���� ���� UI ��ũ��Ʈ ����
    private ScoreManager scoreManager; // ���� ���� ��ũ��Ʈ ����
    private TimeManager timeManager; // �ð� ���� ��ũ��Ʈ ����

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    public void GameOver()
    {
        int finalScore = scoreManager.GetTotalScore(); // ���� ���� ��������
        float elapsedTime = timeManager.isRunning ? timeManager.GetElapsedTime() : 0f; // ��� �ð� ��������

        gameOverUI.ShowGameOverPanel(finalScore, elapsedTime); // ���� ���� UI ǥ��
    }
}
