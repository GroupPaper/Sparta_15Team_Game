using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using static UnityEngine.Rendering.DebugUI;
using UnityEngine.SocialPlatforms.Impl;

public class GameOverUI : MonoBehaviour
{
    public GameObject gameOverPanel; // ���� ���� �г�
    public TextMeshProUGUI finalScoreText; // ���� ���� �ؽ�Ʈ
    public TextMeshProUGUI finalTimeText; // ���� �ð� �ؽ�Ʈ

    private void Start()
    {
        gameOverPanel.SetActive(false); // ���� ���� �г� ��Ȱ��ȭ
    }

    public void ShowGameOverPanel(int score, float elapsedTime)
    {
        Time.timeScale = 0f;

        // 2) ���� �������ð� �ؽ�Ʈ ����
        finalScoreText.text = $"Score: {score}"; // ���� ���� ǥ��
        int m = Mathf.FloorToInt(elapsedTime / 60f); // �� ���
        int s = Mathf.FloorToInt(elapsedTime % 60f); // �� ���
        finalTimeText.text = $"{m:D2}:{s:D2}"; // ���� �ð� ǥ��

        gameOverPanel.SetActive(true);
    }
}
