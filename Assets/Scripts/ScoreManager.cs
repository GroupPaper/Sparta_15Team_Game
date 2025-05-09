using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText; // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝
    public TextMeshProUGUI bestScoreText; // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝

    public float scoreMultiplier = 1f; // О©╫й╢О©╫ О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫О©╫О©╫

    private int timeScore = 0; // О©╫О©╫О©╫О©╫
    private int itemScore = 0; // О©╫О©╫О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫
    private float timeAccumulator = 0; // О©╫О©╫О©╫О©╫ О©╫ц╟О©╫

    private int bestScore = 0; // О©╫ж╟О©╫ О©╫О©╫О©╫О©╫


    private void Start()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0); // О©╫О©╫О©╫О©╫О©╫ О©╫ж╟О©╫ О©╫О©╫О©╫О©╫ О©╫р╥О©╫О©╫О©╫О©╫О©╫
        UpdateBestScoreText(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝

        UpdateScoreText(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝
    }
    private void Update()
    {
        timeAccumulator += Time.deltaTime; // О©╫О©╫О©╫О©╫ О©╫ц╟О©╫ О©╫О©╫О©╫О©╫

        if (timeAccumulator >= 1f) // 1О©╫й╦О©╫О©╫О©╫ О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫
        {
            int gained = Mathf.FloorToInt(timeAccumulator * scoreMultiplier); // О©╫О©╫О©╫О©╫ О©╫О©╫О©╫
            timeScore += gained; // О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫
            timeAccumulator = 0; // О©╫О©╫О©╫О©╫ О©╫ц╟О©╫ О©╫й╠О©╫х╜
            UpdateScoreText(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝
        }
    }

    public int GetTotalScore() // ця а║╪Ж ╧щх╞
    {
        return timeScore + itemScore; // ╫ц╟ё а║╪Ж©м ╬фюлеш а║╪Ж гу╩Й
    }

    private void UpdateScoreText()
    {
        int total = timeScore + itemScore; // О©╫О©╫ О©╫О©╫О©╫О©╫ О©╫О©╫О©╫
        Debug.Log("Total Score: " + total); // О©╫О©╫О©╫О©╫О©╫ О©╫н╠О©╫ О©╫О©╫О©╫
        scoreText.text = "Score: " + total.ToString(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝

        if (total > bestScore) // О©╫ж╟О©╫ О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫
        {
            bestScore = total; // О©╫ж╟О©╫ О©╫О©╫О©╫О©╫ О©╫О©╫О©╫О©╫
            PlayerPrefs.SetInt("BestScore", bestScore); // О©╫О©╫О©╫О©╫
            UpdateBestScoreText(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝
        }
    }

    private void UpdateBestScoreText()
    {
        bestScoreText.text = "Best Score: " + bestScore.ToString(); // UI О©╫ь╫О©╫ф╝ О©╫О©╫О©╫О©╫О©╫О©╫ф╝
    }

    public void AddItemScore(int bonus) // Л∙└Л²╢М┘° К╗╧Л°╪К╘╢ Л═░Л┬≤ Л┐│Л┼╧М∙≤К┼■ Л╫■К⌠°
    {
        itemScore += bonus;
        UpdateScoreText();
    }
}