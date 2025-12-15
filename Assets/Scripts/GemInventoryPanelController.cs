using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemInventoryPanelController : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false); // 자기 자신
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            gameObject.SetActive(!gameObject.activeSelf);
        }
    }
}
