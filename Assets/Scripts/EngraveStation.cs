using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngraveStation : MonoBehaviour
{
    public EngravePuzzleManager engravingUI;

    private Inventory playerInventory;
    private bool playerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<Inventory>();
            playerInRange = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
            playerInRange = false;
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryEngrave();
        }
    }

    void TryEngrave()
    {
        if (!playerInventory.Consume(BlockType.SilverIngot, 1))
        {
            Debug.Log("세공할 은 주괴가 없습니다!");
            return;
        }

       
        engravingUI.StartEngraving(playerInventory, BlockType.SilverIngot);
    }
}
