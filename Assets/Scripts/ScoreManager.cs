using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    public class ScoreManager : MonoBehaviour
    {
        public TextMeshProUGUI scoreText; // UI 텍스트 컴포넌트
        public float scoreMultiplier = 1f; // 초당 점수 증가율

        private int timeScore = 0; // 점수
        private int itemScore = 0; // 아이템 점수
        private float timeAccumulator = 0; // 누적 시간

        private void Update()
        {
            timeAccumulator += Time.deltaTime; // 누적 시간 증가

            if (timeAccumulator >= 1f) // 1초마다 점수 증가
            {
                int gained = Mathf.FloorToInt(timeAccumulator * scoreMultiplier);
                timeScore += gained; // 점수 증가
                timeAccumulator -= gained; // 누적 시간 감소
                UpdateScoreText(); // UI 텍스트 업데이트
            }
        }

        private void UpdateScoreText()
        {
            int total = timeScore + itemScore; // 총 점수 계산
            scoreText.text = total.ToString(); // UI 텍스트 업데이트
        }
    }