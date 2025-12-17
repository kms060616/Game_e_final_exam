using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OrderNPC : MonoBehaviour
{
    public GameObject orderPanel;       // UI 패널
    public Text orderText;              // "골드 반지 x1 만들어 주세요" 이런 텍스트
    public Text buttonText;             // 버튼 텍스트 (의뢰 받기 / 완료하기)
    public Button actionButton;

    Inventory playerInventory;

    void Start()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null) playerInventory = player.GetComponent<Inventory>();

        orderPanel.SetActive(false);
        actionButton.onClick.AddListener(OnActionButton);
    }

    void Update()
    {
        // 간단한 예시: 플레이어가 가까이 있고 E 누르면 오픈
        if (Input.GetKeyDown(KeyCode.R))
        {
            TogglePanel();
        }
    }

    void TogglePanel()
    {
        bool show = !orderPanel.activeSelf;
        orderPanel.SetActive(show);

        if (show)
        {
            RefreshUI();
        }
    }

    void RefreshUI()
    {
        var gm = GameManager.Instance;

        if (!gm.HasActiveOrder)
        {
            orderText.text = "새 의뢰를 받겠습니까?\n예) 골드 반지 x1 제작";
            buttonText.text = "의뢰 받기";
            actionButton.interactable = true;   //  항상 가능
        }
        else
        {
            var o = gm.currentOrder;
            string itemName = GetOrderName(o.type);

            orderText.text = $"현재 의뢰: {itemName} x{o.count}\n보상: {o.reward} G";

            bool can = CanCompleteOrder(o);
            buttonText.text = can ? "의뢰 완료" : "아직 미완료";
            actionButton.interactable = can;    // 완료 가능할 때만 클릭 가능
        }
    }

    void OnActionButton()
    {
        var gm = GameManager.Instance;

        // 의뢰 없음 → 새 의뢰 생성
        if (!gm.HasActiveOrder)
        {
            gm.currentOrder = CreateRandomOrder();
            RefreshUI();
        }
        else
        {
            // 의뢰 있음 → 완료 가능하면 처리
            if (CanCompleteOrder(gm.currentOrder))
            {
                CompleteOrder(gm.currentOrder);
                gm.currentOrder.isCompleted = true;
                gm.currentOrder = null;
                RefreshUI();
            }
        }
    }

    OrderData CreateRandomOrder()
    {
        // 간단한 랜덤 예시
        AccessoryOrderType[] possible =
        {
            AccessoryOrderType.GoldRing,
            AccessoryOrderType.GoldNecklace,
            AccessoryOrderType.SilverRing,
            AccessoryOrderType.SilverNecklace
        };

        int idx = Random.Range(0, possible.Length);
        int count = 1; // 나중에 난이도 따라 1~3개로 조정 가능
        int reward = 100 * count; // 대충 예시

        return new OrderData
        {
            type = possible[idx],
            count = count,
            reward = reward,
            isCompleted = false
        };
    }

    bool CanCompleteOrder(OrderData order)
    {
        if (playerInventory == null) return false;

        BlockType required = OrderTypeToBlockType(order.type);
        return playerInventory.GetCount(required) >= order.count;
    }

    void CompleteOrder(OrderData order)
    {
        BlockType required = OrderTypeToBlockType(order.type);

        // 인벤토리에서 소비
        playerInventory.Consume(required, order.count);

        // 돈 지급
        GameManager.Instance.AddMoney(order.reward);

        Debug.Log($"[Order] 의뢰 완료! +{order.reward} G (총 {GameManager.Instance.money})");
    }

    BlockType OrderTypeToBlockType(AccessoryOrderType type)
    {
        switch (type)
        {
            case AccessoryOrderType.GoldRing: return BlockType.GoldRing;
            case AccessoryOrderType.GoldNecklace: return BlockType.GoldNecklace;
            case AccessoryOrderType.SilverRing: return BlockType.SilverRing;
            case AccessoryOrderType.SilverNecklace: return BlockType.SilverNecklace;
            default: return BlockType.Dirt;
        }
    }

    string GetOrderName(AccessoryOrderType type)
    {
        switch (type)
        {
            case AccessoryOrderType.GoldRing: return "골드 반지";
            case AccessoryOrderType.GoldNecklace: return "골드 목걸이";
            case AccessoryOrderType.SilverRing: return "실버 반지";
            case AccessoryOrderType.SilverNecklace: return "실버 목걸이";
            default: return "알 수 없음";
        }
    }
}
