using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryPanelController : MonoBehaviour
{
    [SerializeField] private GameObject gemPanel;  // GemInventoryPanel 오브젝트

    void Awake()
    {
        // 씬에서 못 찾았으면 이름으로라도 찾기(선택)
        if (gemPanel == null)
            gemPanel = GameObject.Find("GemInventoryPanel");

        if (gemPanel != null)
            gemPanel.SetActive(false);
        else
            Debug.LogWarning("[GemUI] gemPanel이 연결되지 않았습니다!");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            if (gemPanel == null)
            {
                gemPanel = GameObject.Find("GemInventoryPanel");
                if (gemPanel == null) return;
            }

            bool next = !gemPanel.activeSelf;
            gemPanel.SetActive(next);

            //켜질 때마다 인벤/젬UI 갱신
            if (next)
            {
                var inv = FindObjectOfType<Inventory>(true);
                var gemUI = gemPanel.GetComponentInChildren<GemInventoryUI>(true); // 혹은 GemInventoryUI 대신 너가 쓰는 스크립트명
                if (inv != null && gemUI != null)
                    gemUI.UpdateInventory(inv);
            }
        }
    }
}
