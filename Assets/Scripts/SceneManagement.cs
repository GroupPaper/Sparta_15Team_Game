using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    [SerializeField] private AudioClip clickSfx; // 클릭 사운드 효과


    // 타이틀 씬으로 이동
    public void StartScene()
    {
        PlayClick(); // 클릭 사운드 재생
        SceneManager.LoadScene("StartScene");
    }

    // 메인 씬(게임 화면)으로 이동
    public void MainScene()
    {
        PlayClick(); // 클릭 사운드 재생
        SceneManager.LoadScene("MainSceneRyu");
    }

    public void Retry()
    {
        PlayClick(); // 클릭 사운드 재생
        Time.timeScale = 1f; // 게임 재개
        SceneManager.LoadScene("MainSceneRyu"); // 메인 씬으로 이동
    }

    // 게임 오버 씬으로 이동
    public void Exit()
    {
        PlayClick(); // 클릭 사운드 재생
        // UnityEditor.EditorApplication.isPlaying = false; // 에디터에서 실행 중지
        // Application.Quit(); // 빌드된 게임에서 종료
        // 게임 종료
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void PlayClick()
    {
        Debug.Log($"PlayClick 호출! clickSfx={clickSfx}, SoundManager.Instance={(SoundManager.Instance != null)}");
        // 클릭 사운드 재생
        if (clickSfx != null && SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX(clickSfx);
        }
    }
}
