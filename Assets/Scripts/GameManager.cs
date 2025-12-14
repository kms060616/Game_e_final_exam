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
    public int reward;         // º¸»ó µ·
    public bool isCompleted;
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int money = 0;              // ÃÑ º¸À¯ µ·
    public OrderData currentOrder;     // ÇöÀç ÁøÇà ÁßÀÎ ÀÇ·Ú

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
}
