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

    void Start()
    {
        currentHP = maxHP; // ���� HP�� �ִ� HP�� �ʱ�ȭ
        hpBar.minValue = 0; // �����̴� �ּҰ�
        hpBar.maxValue = maxHP; // �����̴� �ִ밪
        UpdateHPBarUI(); // HP �� ������Ʈ
    }

    private void Update()
    {
        if(currentHP > 0)
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
