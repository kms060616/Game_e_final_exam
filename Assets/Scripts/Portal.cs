using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public string targetSceneName;
    public string targetSpawnId;
    public KeyCode interactKey = KeyCode.E;

    private bool canUse;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            canUse = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            canUse = false;
    }

    private void Update()
    {
        if (!canUse) return;

        if (Input.GetKeyDown(interactKey))
        {
            if (SceneTransitManager.Instance == null)
            {
                Debug.LogError("[Portal] SceneTransitManager가 씬에 없습니다!");
                return;
            }

            SceneTransitManager.Instance.LoadScene(targetSceneName, targetSpawnId);
        }
    }
}
