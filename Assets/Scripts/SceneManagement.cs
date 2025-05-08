using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // Ÿ��Ʋ ������ �̵�
    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    // ���� ��(���� ȭ��)���� �̵�
    public void MainScene()
    {
        SceneManager.LoadScene("MainSceneRyu");
    }

    // ���� ���� ������ �̵�
    public void Exit()
    {
        // UnityEditor.EditorApplication.isPlaying = false; // �����Ϳ��� ���� ����
        // Application.Quit(); // ����� ���ӿ��� ����
        // ���� ����
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
