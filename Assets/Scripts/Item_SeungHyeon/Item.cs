using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum ItemEffectType {Heal, Speed, Score, MegaHeal, MegaSpeed, MegaScore}

public class Item : MonoBehaviour
{
    public ItemEffectType itemEffect;
    
    private void OnTriggerEnter2D(Collider2D other) // 캐릭터가 아이템이랑 충돌하는 경우
    {
        Player player = other.GetComponent<Player>();
        if(other.CompareTag("Player"))
        {
            ApplyItemEffect(player); // 플레이어에게 아이템 효과를 적용한다.
            Destroy(gameObject); // 아이템 제거
        }
    }

    private void ApplyItemEffect(Player player)
    {
        switch (itemEffect)
        {
            case ItemEffectType.Heal:
            player.HealFromItem(10f); // HpBar쪽에 연결됨됨
            break;

            case ItemEffectType.MegaHeal:
            player.HealFromItem(30f);
            break;

        case ItemEffectType.Speed:
            player.ApplySpeedBuffFromItem(5f, 3f); // 3초동안 이동속도 5 증가 MovementController 쪽 연결결
            break;

        case ItemEffectType.MegaSpeed:
            player.ApplySpeedBuffFromItem(10f, 5f); // 5초동안 이동속도 10 증가
            break;

        case ItemEffectType.Score:
            player.AddScoreFromItem(100); // ScoreManager 연결결
            break;

        case ItemEffectType.MegaScore:
            player.AddScoreFromItem(300);
            break;
        };
    }
}
