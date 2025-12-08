using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngraveStation : MonoBehaviour
{
    public EngraveSelectUI selectUI;

    private Inventory playerInventory;
    private bool playerInRange;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = other.GetComponent<Inventory>();
            playerInRange = true;

            Debug.Log("세공대 접근: 인벤토리 연결됨");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInventory = null;
            playerInRange = false;

            Debug.Log("세공대 범위 벗어남");
        }
    }

    void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (selectUI != null && playerInventory != null)
            {
                selectUI.Open(playerInventory); 
                Debug.Log("세공 선택 UI 오픈");
            }
            else
            {
                Debug.LogWarning("selectUI 또는 playerInventory가 NULL");
            }
        }
    }
}
