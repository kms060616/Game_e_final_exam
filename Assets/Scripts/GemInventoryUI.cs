using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemInventoryUI : MonoBehaviour
{
    [Header("아이콘 스프라이트")]
    public Sprite GoldOre;
    public Sprite SilverOre;
    public Sprite DiamondOre;
    public Sprite GoldIngot;
    public Sprite SilverIngot;
    public Sprite DiamondGem;
    public Sprite GoldRing;
    public Sprite SilverRing;
    public Sprite SilverNecklace;
    public Sprite GoldNecklace;

    [Header("슬롯 구조")]
    public List<Transform> Slots = new List<Transform>(); // 보석용 슬롯들
    public GameObject SlotItemPrefab;                     // 기존 SlotItemPrefab 재사용

    private List<GameObject> items = new List<GameObject>();

    public void UpdateInventory(Inventory inven)
    {
        // 기존 슬롯 아이템 삭제
        foreach (var go in items)
            Destroy(go);
        items.Clear();

        int idx = 0;

        foreach (var pair in inven.items)
        {
            BlockType type = pair.Key;
            int count = pair.Value;

            //보석/광물만 필터링
            if (!IsGem(type))
                continue;

            if (idx >= Slots.Count)
                break; // 슬롯이 부족하면 여기까지만

            var go = Instantiate(SlotItemPrefab, Slots[idx]);
            go.transform.localPosition = Vector3.zero;

            var slot = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (type)
            {
                case BlockType.GoldOre:
                    slot.ItemSetting(GoldOre, "x" + count, type);
                    break;
                case BlockType.SilverOre:
                    slot.ItemSetting(SilverOre, "x" + count, type);
                    break;
                case BlockType.DiamondOre:
                    slot.ItemSetting(DiamondOre, "x" + count, type);
                    break;
                case BlockType.GoldIngot:
                    slot.ItemSetting(GoldIngot, "x" + count, type);
                    break;
                case BlockType.SilverIngot:
                    slot.ItemSetting(SilverIngot, "x" + count, type);
                    break;
                case BlockType.DiamondGem:
                    slot.ItemSetting(DiamondGem, "x" + count, type);
                    break;
                case BlockType.GoldRing:
                    slot.ItemSetting(GoldRing, "x" + count, type);
                    break;
                case BlockType.GoldNecklace:
                    slot.ItemSetting(GoldNecklace, "x" + count, type);
                    break;
                case BlockType.SilverNecklace:
                    slot.ItemSetting(SilverNecklace, "x" + count, type);
                    break;
                case BlockType.SilverRing:
                    slot.ItemSetting(SilverRing, "x" + count, type);
                    break;
            }

            idx++;
        }
    }

    bool IsGem(BlockType type)
    {
        //여기서 "보석 전용 인벤토리"에 들어갈 타입만 골라줌
        switch (type)
        {
            case BlockType.GoldOre:
            case BlockType.SilverOre:
            case BlockType.DiamondOre:
            case BlockType.GoldIngot:
            case BlockType.SilverIngot:
            case BlockType.DiamondGem:
            case BlockType.GoldRing:
            case BlockType.GoldNecklace:
            case BlockType.SilverNecklace:
            case BlockType.SilverRing:
                return true;
            default:
                return false;
        }
    }
}
