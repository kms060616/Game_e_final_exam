using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindingStation : MonoBehaviour
{
    [Header("UI 패널 연결")]
    public GameObject grindingPanel;        
    private JewelGrinding jewelGrinding; 

    private bool playerInRange = false;
    private Inventory playerInventory;      

    private void Start()
    {
        if (grindingPanel != null)
        {
            jewelGrinding = grindingPanel.GetComponent<JewelGrinding>();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInventory = other.GetComponent<Inventory>(); 
            Debug.Log("연마대 근처에 접근. E키로 연마 가능");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
            Debug.Log("연마대 범위에서 벗어남");
        }
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            TryOpenGrinding();
        }
    }

    void TryOpenGrinding()
    {
        if (grindingPanel == null || jewelGrinding == null || playerInventory == null)
        {
            Debug.LogWarning("연마대 세팅이 안되어 있습니다.");
            return;
        }

        if (!playerInventory.Consume(BlockType.GoldOre, 1))
        {
            Debug.Log("연마할 GoldOre가 없습니다!");
            return;
        }

        
        grindingPanel.SetActive(true);
        jewelGrinding.StartGrinding(playerInventory, BlockType.GoldIngot);
    }
}
