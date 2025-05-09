using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public enum ItemEffectType {Heal, Speed, Score}

public class Item : MonoBehaviour
{
    public ItemEffectType itemEffect;
    public int value;
    public float duration;
    public Player player; // 임시로 넣음 캐릭터 코드 구현하면 맞춰서 수정해야함
    
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
            // HPBar.Heal(10f); 스태틱 처리 필요함
            break;

        case ItemEffectType.Speed:
            // MovementController.ApplySpeedItemBuff(5f, 3f); 3초동안 이동속도 5 증가가
            break;

        case ItemEffectType.Score:
            // ScoreManager.AddItemScore(30);
            break;
        };
    }
}
