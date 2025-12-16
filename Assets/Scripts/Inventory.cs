using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Dictionary<BlockType, int> items = new();
    InventoryUI invenUI;
    GemInventoryUI gemUI;



    public void Add(BlockType type, int count = 1)
    {
        if (!items.ContainsKey(type)) items[type] = 0;
        items[type] += count;
        Debug.Log($"[Inventory] + {count} {type} (รั {items[type]})");

        invenUI.UpdateInventory(this);
        if (gemUI != null) gemUI.UpdateInventory(this);
    }

    public bool Consume(BlockType type, int count = 1)
    {
        if (!items.TryGetValue(type, out var have) || have < count) return false;
        items[type] = have - count;
        Debug.Log($"[Inventory] -{count} {type} (รั {items[type]})");

        if (items[type] == 0)
        {
            items.Remove(type);
            invenUI.selectedIndex = -1;
            invenUI.ResetSelection();

        }
        invenUI.UpdateInventory(this);
        if (gemUI != null) gemUI.UpdateInventory(this);

        return true;


    }

    public int GetCount(BlockType type)
    {
        if (items.TryGetValue(type, out var count))
            return count;
        return 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
        gemUI = FindObjectOfType<GemInventoryUI>();

        invenUI = FindObjectOfType<InventoryUI>(true);

        Add(BlockType.Dirt, 10);
        Add(BlockType.Grass, 10);

        if (invenUI != null) invenUI.UpdateInventory(this);

        Debug.Log("[Inventory] Start complete");


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
