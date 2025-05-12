using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private AudioClip clickSfx; // Ŭ�� ���� ȿ��


    // Ÿ��Ʋ ������ �̵�
    public void StartScene()
    {
        PlayClick(); // Ŭ�� ���� ���
        SceneManager.LoadScene("StartScene");
    }

    // ���� ��(���� ȭ��)���� �̵�
    public void MainScene()
    {
        PlayClick(); // Ŭ�� ���� ���
        SceneManager.LoadScene("MainSceneRyu");
    }

    public void Retry()
    {
        PlayClick(); // Ŭ�� ���� ���
        Time.timeScale = 1f; // ���� �簳
        SceneManager.LoadScene("MainSceneRyu"); // ���� ������ �̵�
    }

    // ���� ���� ������ �̵�
    public void Exit()
    {
        PlayClick(); // Ŭ�� ���� ���
        // UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ����
        // Application.Quit(); // ����� ���ӿ��� ����
        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PlayClick()
    {
        Debug.Log($"PlayClick ȣ��! clickSfx={clickSfx}, SoundManager.Instance={(SoundManager.Instance != null)}");
        // Ŭ�� ���� ���
        if (clickSfx != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(clickSfx);
        }
    }
}
