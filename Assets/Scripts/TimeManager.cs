using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimeManager : MonoBehaviour
{
    public TextMeshProUGUI timeText; // UI �ؽ�Ʈ ������Ʈ
    private float elapsedTime = 0f; // ��� �ð�
    public bool isRunning = true; // Ÿ�̸� ���� ����

    private void Update()
    {
        if(!isRunning) return; // Ÿ�̸Ӱ� ���� ���� �ƴ� ��� ������Ʈ ���� ����

        elapsedTime += Time.deltaTime; // ��� �ð� ����
        int minutes = Mathf.FloorToInt(elapsedTime / 60); // �� ���
        int seconds = Mathf.FloorToInt(elapsedTime % 60); // �� ���
        timeText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds); // UI �ؽ�Ʈ ������Ʈ
    }
}
