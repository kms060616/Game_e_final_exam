using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Progress;

public class InventoryUI : MonoBehaviour
{
    public Sprite Dirt;
    public Sprite Grass;
    public Sprite Water;
    public Sprite GoldOre;
    public Sprite GoldIngot;
    public Sprite SilverOre;
    public Sprite SilverIgot;
    public Sprite GoldRing;
    public Sprite GoldNecklace;
    public Sprite SilverRing;
    public Sprite SilverNecklace;
    public Sprite DiamondOre;
    public Sprite DiamondGem;
    public List<Transform> Slot = new List<Transform>();
    public GameObject SlotItem;
    List<GameObject> items = new List<GameObject>();

    public int selectedIndex = -1;

    public void UpdateInventory(Inventory myInven)
    {
        foreach (var slotItems in items)
        {
            Destroy(slotItems);

            
        }
        items.Clear();

        int idx = 0;
        foreach (var item in myInven.items)
        {

            if (idx >= Slot.Count)
                break;

            if (IsGemItem(item.Key))
                continue;

            bool IsGemItem(BlockType type)
            {
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
                        return true;   //보석/광물은 일반 인벤에서 숨김
                    default:
                        return false;
                }
            }

            var go = Instantiate(SlotItem, Slot[idx].transform);
            go.transform.localPosition = Vector3.zero;
            SlotItemPrefab sltem = go.GetComponent<SlotItemPrefab>();
            items.Add(go);

            switch (item.Key)
            {
                case BlockType.Dirt:
                    sltem.ItemSetting(Dirt, "x" + item.Value.ToString(), item.Key);
                    break;

                case BlockType.Water:
                    sltem.ItemSetting(Water, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.Grass:
                    sltem.ItemSetting(Grass, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.GoldOre:
                    sltem.ItemSetting(GoldOre, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.GoldIngot:
                    sltem.ItemSetting(GoldIngot, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.SilverOre:
                    sltem.ItemSetting(SilverOre, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.SilverIngot:
                    sltem.ItemSetting(SilverIgot, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.GoldRing:
                    sltem.ItemSetting(GoldRing, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.GoldNecklace:
                    sltem.ItemSetting(GoldNecklace, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.SilverRing:
                    sltem.ItemSetting(SilverRing, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.SilverNecklace:
                    sltem.ItemSetting(SilverNecklace, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.DiamondOre:
                    sltem.ItemSetting(DiamondOre, "x" + item.Value.ToString(), item.Key);
                    break;
                case BlockType.DiamondGem:
                    sltem.ItemSetting(DiamondGem, "x" + item.Value.ToString(), item.Key);
                    break;


            }


            idx++;

        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < Mathf.Min(9, Slot.Count); i++)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1 + i))
            {
                SetSlectedIndex(i);
            }
        }
    }

    public void SetSlectedIndex(int idx)
    {
        ResetSelection();
        if (selectedIndex == idx)
        {
            selectedIndex = -1;
        }
        else
        {
            if (idx >= items.Count)
            {
                selectedIndex = -1;
            }
            else
            {
                SetSelection(idx);
                selectedIndex = idx;

            }
        }
   
    }

    public void ResetSelection()
    {
        foreach(var slot in Slot)
        {
            slot.GetComponent<Image>().color = Color.white;
        }
    }

    void SetSelection(int _idx)
    {
        Slot[_idx].GetComponent<Image>().color = Color.yellow;
    }

    public BlockType GetInventorySlot()
    {
        return items[selectedIndex].GetComponent<SlotItemPrefab>().blockType;
    }

    void OnEnable()
    {
        if (Slot == null || Slot.Count == 0)
        {
            Debug.LogWarning("[InventoryUI] Slot 리스트가 비어있어서 UpdateInventory를 건너뜀");
            return;
        }

        for (int i = 0; i < Slot.Count; i++)
        {
            if (Slot[i] == null)
            {
                Debug.LogWarning($"[InventoryUI] Slot[{i}]가 비어있어서 UpdateInventory를 건너뜀");
                return;
            }
        }

        var inv = FindObjectOfType<Inventory>(true);
        if (inv != null) UpdateInventory(inv);
    }
}
