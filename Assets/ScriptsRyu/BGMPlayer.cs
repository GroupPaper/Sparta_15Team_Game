using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip bgmClip; // BGM 클립

    private void Start()
    {
        SoundManager.Instance.PlayBGM(bgmClip, loop: true); // BGM 재생
    }
}
