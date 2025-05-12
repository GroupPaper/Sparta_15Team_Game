using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // �̱��� �ν��Ͻ�

    public AudioSource bgmSource; // BGM �ҽ�
    public AudioSource sfxSource; // SFX �ҽ�

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // �̱��� �ν��Ͻ� ����
            DontDestroyOnLoad(gameObject); // �� ��ȯ �� �ı����� �ʵ��� ����
        }
        else
        {
            Destroy(gameObject); // �̹� �����ϴ� �ν��Ͻ��� ������ �ı�
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if (clip == null)
        {
            return; // Ŭ���� null�̸� �ƹ��͵� ���� ����
        }
        bgmSource.clip = clip; // BGM �ҽ��� Ŭ�� ����
        bgmSource.loop = loop; // ���� ����
        bgmSource.Play(); // BGM ���
    }

    public void StopBGM()
    {
        bgmSource.Stop(); // BGM ����
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip == null)
        {
            return; // Ŭ���� null�̸� �ƹ��͵� ���� ����
        }
        sfxSource.PlayOneShot(clip); // SFX ���
    }
}
