using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class HPBar : MonoBehaviour
{
    public Slider hpBar; // 체력바 UI 슬라이더

    public int maxHP = 100; 
    private float currentHP; // 현재 체력

    public float hpDecreaseRatePerSec = 5f; // 체력 감소 속도

    private bool isHealing = false; // 체력 회복 효과 딜레이시 사용
    private bool isDamage = false;

    void Start()
    {
        currentHP = maxHP; 
        hpBar.minValue = 0;  
        hpBar.maxValue = maxHP; 
        UpdateHPBarUI();
    }

    private void Update()
    {
        if(currentHP > 0 && !isHealing) // 체력 회복 효과 사용 아닐때만 피가 감소하도록록
        {
            currentHP -= hpDecreaseRatePerSec * Time.deltaTime; // 체력 감소
            currentHP = Mathf.Clamp(currentHP, 0, maxHP); // 체력 제한
            UpdateHPBarUI();

            if ((currentHP <= 0))
            {
                FindObjectOfType<GameManager>().GameOver(); // 체력이 0이 되면 게임 오버
            }
        }
    }

    private void UpdateHPBarUI()
    {
        hpBar.value = currentHP; // 체력바 UI 업데이트
    }

    public void Heal(float healValue) // 체력 회복 아이템 사용
    {
        StartCoroutine(HealWithDelay(healValue));
    }
    public void Damege(float damage) // 피격 데미지
    {
        StartCoroutine(DamegeDelay(damage));
    }

    private IEnumerator DamegeDelay(float damage) // 체력바 변화와 안겹치게 코루틴 사용
    {
        isDamage = true;
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP);
        UpdateHPBarUI();
        yield return new WaitForSeconds(0.1f); // 아주 잠깐 기다려줌
        isDamage = false;
    }

    private IEnumerator HealWithDelay(float healValue) // 체력바 변화와 안겹치게 코루틴 사용
    {
        isHealing = true;
        currentHP = Mathf.Clamp(currentHP + healValue, 0, maxHP);
        UpdateHPBarUI();
        yield return new WaitForSeconds(0.1f); // 아주 잠깐 기다려줌
        isHealing = false;
    }
}
