using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmeltingFurnace : MonoBehaviour
{
    [Header("제련 미니게임 패널")]
    public SmeltingMiniGame smeltingMiniGame;   // SmeltingPanel에 붙어있는 스크립트 참조

    private bool playerInRange = false;
    private Inventory playerInventory;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<Inventory>();
            Debug.Log("제련대 근처: E키로 제련 시작 가능");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
            Debug.Log("제련대 범위 벗어남");
        }
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryStartSmelting();
        }
    }

    void TryStartSmelting()
    {
        if (smeltingMiniGame == null || playerInventory == null)
        {
            Debug.LogWarning("제련대 세팅이 안 되어 있습니다.");
            return;
        }

        //  1) 먼저 SilverOre 1개 있는지 확인하고, 있으면 소비
        if (!playerInventory.Consume(BlockType.SilverOre, 1))
        {
            Debug.Log("제련할 SilverOre가 없습니다!");
            return;
        }

        //  2) 미니게임 시작: 성공 시 SilverIngot 지급
        smeltingMiniGame.StartSmelting(playerInventory, BlockType.SilverIngot);
    }
}
