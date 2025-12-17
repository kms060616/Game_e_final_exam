using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AccessoryOrderType
{
    None,
    GoldRing,
    GoldNecklace,
    SilverRing,
    SilverNecklace
}

[System.Serializable]
public class OrderData
{
    public AccessoryOrderType type;
    public int count;
    public int reward;         // 보상 돈
    public bool isCompleted;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;              // 총 보유 돈 (기존 유지)
    public OrderData currentOrder;     // 현재 진행 중인 의뢰 (기존 유지)

    //추가: 돈 변경 이벤트 (UI 자동 갱신용)
    public event Action<int> OnMoneyChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public bool HasActiveOrder => currentOrder != null && !currentOrder.isCompleted;

    //추가: 돈 벌기/쓰기 함수 (앞으로는 이걸로 money 변경 추천)
    public void AddMoney(int amount)
    {
        money += amount;
        OnMoneyChanged?.Invoke(money);
        Debug.Log($"[Money] +{amount} (총 {money})");
    }

    public bool SpendMoney(int amount)
    {
        if (money < amount) return false;
        money -= amount;
        OnMoneyChanged?.Invoke(money);
        Debug.Log($"[Money] -{amount} (총 {money})");
        return true;
    }

    //추가: 혹시 기존 코드가 money를 직접 바꾼다면, UI 강제 갱신용
    public void RefreshMoneyUI()
    {
        OnMoneyChanged?.Invoke(money);
    }
}
