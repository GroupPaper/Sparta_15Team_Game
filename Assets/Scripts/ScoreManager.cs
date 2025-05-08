using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI �ؽ�Ʈ ������Ʈ
    public TextMeshProUGUI bestScoreText; // UI �ؽ�Ʈ ������Ʈ

    public float scoreMultiplier = 1f; // �ʴ� ���� ������

    private int timeScore = 0; // ����
    private int itemScore = 0; // ������ ����
    private float timeAccumulator = 0; // ���� �ð�

    private int bestScore = 0; // �ְ� ����

    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0); // ����� �ְ� ���� �ҷ�����
        UpdateBestScoreText(); // UI �ؽ�Ʈ ������Ʈ

        UpdateScoreText(); // UI �ؽ�Ʈ ������Ʈ
    }
    private void Update()
    {
        timeAccumulator += Time.deltaTime; // ���� �ð� ����

        if (timeAccumulator >= 1f) // 1�ʸ��� ���� ����
        {
            int gained = Mathf.FloorToInt(timeAccumulator * scoreMultiplier);
            timeScore += gained; // ���� ����
            timeAccumulator -= gained; // ���� �ð� ����
            UpdateScoreText(); // UI �ؽ�Ʈ ������Ʈ
        }
    }

    private void UpdateScoreText()
    {
        int total = timeScore + itemScore; // �� ���� ���
        scoreText.text = total.ToString(); // UI �ؽ�Ʈ ������Ʈ

        if (total > bestScore) // �ְ� ���� ����
        {
            bestScore = total; // �ְ� ���� ����
            PlayerPrefs.SetInt("BestScore", bestScore); // ����
            UpdateBestScoreText(); // UI �ؽ�Ʈ ������Ʈ
        }
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best: " + bestScore.ToString(); // UI �ؽ�Ʈ ������Ʈ
    }
}