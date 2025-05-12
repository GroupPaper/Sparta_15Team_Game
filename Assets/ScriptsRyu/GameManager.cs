using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameOverUI gameOverUI; // ���� ���� UI ��ũ��Ʈ ����
    public AudioClip gameOverBgm; // ���� ���� BGM Ŭ��
    private ScoreManager scoreManager; // ���� ���� ��ũ��Ʈ ����
    private TimeManager timeManager; // �ð� ���� ��ũ��Ʈ ����

    private void Awake()
    {
        scoreManager = FindObjectOfType<ScoreManager>();
        timeManager = FindObjectOfType<TimeManager>();
    }

    public void GameOver()
    {
        SoundManager.Instance.StopBGM(); // ���� BGM ����

        if(gameOverBgm != null)
        {
            SoundManager.Instance.PlayBGM(gameOverBgm, loop: false); // ���� ���� BGM ���
        }

        int finalScore = scoreManager.GetTotalScore(); // ���� ���� ��������
        float elapsedTime = timeManager.isRunning ? timeManager.GetElapsedTime() : 0f; // ��� �ð� ��������

        gameOverUI.ShowGameOverPanel(finalScore, elapsedTime); // ���� ���� UI ǥ��
    }
}
