using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrindingStation : MonoBehaviour
{
    [Header("UI 패널 연결")]
    public GameObject grindingPanel;          // Canvas 안의 GrindingPanel
    private JewelGrinding jewelGrinding;      // 패널에 있는 스크립트

    private bool playerInRange = false;

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
            Debug.Log("연마대 근처에 접근함. E키로 상호작용 가능");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            Debug.Log("연마대 범위에서 벗어남");
        }
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            OpenGrinding();
        }
    }

    void OpenGrinding()
    {
        if (grindingPanel == null || jewelGrinding == null) return;

        grindingPanel.SetActive(true);   // 패널 켜기
        jewelGrinding.StartGrinding();   // 연마 시작
    }
}
