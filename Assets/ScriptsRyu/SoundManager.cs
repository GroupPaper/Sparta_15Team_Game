using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // 싱글톤 인스턴스

    public AudioSource bgmSource; // BGM 소스
    public AudioSource sfxSource; // SFX 소스

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // 싱글톤 인스턴스 설정
            DontDestroyOnLoad(gameObject); // 씬 전환 시 파괴되지 않도록 설정
        }
        else
        {
            Destroy(gameObject); // 이미 존재하는 인스턴스가 있으면 파괴
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null)
        {
            return; // 클립이 null이면 아무것도 하지 않음
        }
        bgmSource.clip = clip; // BGM 소스에 클립 설정
        bgmSource.loop = loop; // 루프 설정
        bgmSource.Play(); // BGM 재생
    }

    public void StopBGM()
    {
        bgmSource.Stop(); // BGM 정지
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            return; // 클립이 null이면 아무것도 하지 않음
        }
        sfxSource.PlayOneShot(clip); // SFX 재생
    }
}
