using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum ItemEffectType {Heal, Speed, Score}

public class Item : MonoBehaviour
{
    public ItemEffectType itemEffect;
    public Player player; 
    
    private void OnTriggerEnter2D(Collider2D other) // 캐릭터가 아이템이랑 충돌하는 경우
    {
        if(other.CompareTag("Player"))
        {
            ApplyItemEffect(player); // 플레이어에게 아이템 효과를 적용한다.
            Destroy(other); // 아이템 제거
        }
    }

    private void ApplyItemEffect(Player player)
    {
        switch (itemEffect)
        {
            case ItemEffectType.Heal:
            player.HealFromItem(10f); // 스태틱 처리 필요함
            break;

        case ItemEffectType.Speed:
            player.ApplySpeedBuffFromItem(5f, 3f); // 3초동안 이동속도 5 증가가
            break;

        case ItemEffectType.Score:
            player.AddScoreFromItem(30);
            break;
        };
    }
}
