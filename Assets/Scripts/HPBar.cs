using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public Slider hpBar; // HP �� �����̴�

    public int maxHP = 100; // �ִ� HP
    private float currentHP; // ���� HP

    public float hpDecreaseRatePerSec = 5f; // �ʴ� HP ���ҷ�

    private bool isHealing = false; // 체력 회복 효과 딜레이시 사용용

    void Start()
    {
        currentHP = maxHP; // ���� HP�� �ִ� HP�� �ʱ�ȭ
        hpBar.minValue = 0; // �����̴� �ּҰ�
        hpBar.maxValue = maxHP; // �����̴� �ִ밪
        UpdateHPBarUI(); // HP �� ������Ʈ
    }

    private void Update()
    {
        if(currentHP > 0 && !isHealing) // 체력 회복 효과 사용 아닐때만 피가 감소하도록록
        {
            currentHP -= hpDecreaseRatePerSec * Time.deltaTime; // HP ����
            currentHP = Mathf.Clamp(currentHP, 0, maxHP); // HP�� 0�� maxHP ���̷� ����
            UpdateHPBarUI(); // HP �� ������Ʈ

            if ((currentHP <= 0))
            {
                SceneManager.LoadScene("StartScene"); // HP�� 0 ���ϰ� �Ǹ� Ÿ��Ʋ ������ �̵�(���� ���� ���� UI�� ���� ����)
            }
        }
    }

    private void UpdateHPBarUI()
    {
        hpBar.value = currentHP; // �����̴� �� ������Ʈ
    }

    public void Heal(float healValue) // 체력 회복 아이템 사용
    {
        StartCoroutine(HealWithDelay(healValue));
    }

    private IEnumerator HealWithDelay(float healValue) // 체력바 변화와 안겹치게 코루틴 사용
    {
        isHealing = true;
        currentHP = Mathf.Clamp(currentHP + healValue, 0, maxHP);
        UpdateHPBarUI();
        yield return new WaitForSeconds(0.1f); // 아주 잠깐 기다려줌
        isHealing = false;
    }

    /*
    // HP�� ���ҽ�Ű�� �޼���
    public void TakeDamage(int damage)
    {
        currentHP = Mathf.Clamp(currentHP - damage, 0, maxHP); // HP ����
        UpdateHPBarUI(); // HP �� ������Ʈ
    }

    public void Heal(int healAmount)
    {
        currentHP = Mathf.Clamp(currentHP + healAmount, 0, maxHP); // HP ȸ��
        UpdateHPBarUI(); // HP �� ������Ʈ
    }
    */
}
