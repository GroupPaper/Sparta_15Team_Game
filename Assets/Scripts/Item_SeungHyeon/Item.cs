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
    public GameObject player; // 임시로 넣음 캐릭터 코드 구현하면 맞춰서 수정해야함
    
    private void OnTriggerEnter2D(Collider2D other) // 캐릭터가 아이템이랑 충돌하는 경우
    {
        if(other.CompareTag("Player"))
        {
            ApplyItemEffect(other.gameObject); // 추후 효과를 줄 대상을 인스펙터 창에서 연결하도록 설계 필요 other.GetComponent<player>()
            Destroy(gameObject); // 아이템 제거
        }
    }

    private void ApplyItemEffect(GameObject player)
    {
        switch (itemEffect)
        {
            case ItemEffectType.Heal:
            // player.Heal(value);
            break;

        case ItemEffectType.Speed:
            //player.ApplySpeedBoost(value, duration);
            break;

        case ItemEffectType.Score:
            //player.AddScore(value);
            break;
        };
    }
}
