using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitManager : MonoBehaviour
{
    public static SceneTransitManager Instance;
    private string nextSpawnId;

    private void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void LoadScene(string sceneName, string spawnId)
    {
        nextSpawnId = spawnId;
        SceneManager.LoadScene(sceneName);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (string.IsNullOrEmpty(nextSpawnId)) return;

        var player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("[SceneTransitManager] Player 태그 오브젝트를 찾지 못했어요!");
            return;
        }

        var points = GameObject.FindObjectsOfType<SpawnPoint>();
        foreach (var p in points)
        {
            if (p.spawnId == nextSpawnId)
            {
                player.transform.position = p.transform.position;
                break;
            }
        }

        nextSpawnId = null;
    }
}
