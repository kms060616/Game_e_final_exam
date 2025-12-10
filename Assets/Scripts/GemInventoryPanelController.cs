using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryPanelController : MonoBehaviour
{
    public GameObject gemInventoryPanel;

    void Start()
    {
        gemInventoryPanel.SetActive(false);   // 시작할 때 숨김
    }

    void Update()
    {
        //G 키로 보석 인벤 토글
        if (Input.GetKeyDown(KeyCode.G))
        {
            gemInventoryPanel.SetActive(!gemInventoryPanel.activeSelf);
        }
    }
}
