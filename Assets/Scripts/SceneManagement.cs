using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    // 타이틀 씬으로 이동
    public void StartScene()
    {
        SceneManager.LoadScene("StartScene");
    }

    // 메인 씬(게임 화면)으로 이동
    public void MainScene()
    {
        SceneManager.LoadScene("MainSceneRyu");
    }

    // 게임 오버 씬으로 이동
    public void Exit()
    {
        // UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
        // Application.Quit(); // 빌드된 게임에서 종료
        // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
