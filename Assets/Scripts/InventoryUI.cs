using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class InventoryUI : MonoBehaviour
{
    public Sprite Dirt;
    public Sprite Grass;
    public Sprite Water;
    public Sprite GoldOre;
    public Sprite GoldIngot;
    public Sprite SilverOre;
    public Sprite SilverIgot;
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
}
