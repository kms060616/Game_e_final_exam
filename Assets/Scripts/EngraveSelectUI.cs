using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngraveSelectUI : MonoBehaviour
{
    public EngravePuzzleManager puzzleManager;

    public AccessoryData goldRing;
    public AccessoryData goldNecklace;
    public AccessoryData silverRing;
    public AccessoryData silverNecklace;

    private Inventory playerInventory;

    public void Open(Inventory inven)
    {
        playerInventory = inven;
        gameObject.SetActive(true);
    }

    public void SelectGoldRing() => TryStart(goldRing);
    public void SelectGoldNecklace() => TryStart(goldNecklace);
    public void SelectSilverRing() => TryStart(silverRing);
    public void SelectSilverNecklace() => TryStart(silverNecklace);

    void TryStart(AccessoryData data)
    {
        if (data == null)
        {
            Debug.LogError("AccessoryData가 NULL입니다! 버튼에 데이터가 연결 안 됨");
            return;
        }

        if (playerInventory == null)
        {
            Debug.LogError("playerInventory가 NULL입니다! Open()이 제대로 안됨");
            return;
        }

        if (puzzleManager == null)
        {
            Debug.LogError("puzzleManager가 NULL입니다! Inspector 연결 안됨");
            return;
        }

        if (!playerInventory.Consume(data.requiredMaterial, data.requiredCount))
        {
            Debug.Log("재료가 부족합니다!");
            return;
        }

        

        gameObject.SetActive(false);
        puzzleManager.StartEngraving(playerInventory, data);
    }
}
