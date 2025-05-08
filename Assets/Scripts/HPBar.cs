using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider hpBar; // HP 바 슬라이더

    public int maxHP = 100; // 최대 HP
    private float currentHP; // 현재 HP

    public float hpDecreaseRatePerSec = 5f; // 초당 HP 감소량

    void Start()
    {
        currentHP = maxHP; // 현재 HP를 최대 HP로 초기화
        hpBar.minValue = 0; // 슬라이더 최소값
        hpBar.maxValue = maxHP; // 슬라이더 최대값
        UpdateHPBarUI(); // HP 바 업데이트
    }

    private void Update()
    {
        if(currentHP > 0)
        {
            currentHP -= hpDecreaseRatePerSec * Time.deltaTime; // HP 감소
            currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP를 0과 maxHP 사이로 제한
            UpdateHPBarUI(); // HP 바 업데이트

            if ((currentHP <= 0))
            {
                SceneManager.LoadScene("StartScene"); // HP가 0 이하가 되면 타이틀 씬으로 이동(추후 게임 오버 UI로 변경 예정)
            }
        }
    }

    private void UpdateHPBarUI()
    {
        hpBar.value = currentHP; // 슬라이더 값 업데이트
    }

    /*
    // HP를 감소시키는 메서드
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP); // HP 감소
        UpdateHPBarUI(); // HP 바 업데이트
    }

    public void Heal(int healAmount)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP); // HP 회복
        UpdateHPBarUI(); // HP 바 업데이트
    }
    */
}
