using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSingleton : MonoBehaviour
{
    private static PlayerSingleton instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // 새로 생긴 플레이어 제거
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
