using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    void Awake()
    {
        if (moneyText == null) moneyText = GetComponent<TextMeshProUGUI>();
    }

    void OnEnable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnMoneyChanged += Refresh;
        Refresh(GameManager.Instance.money); // 처음 1회 표시
    }

    void OnDisable()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnMoneyChanged -= Refresh;
    }

    void Refresh(int money)
    {
        moneyText.text = $"Gold: {money}";
    }
}
