using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heartbeat : MonoBehaviour
{
    float t;
    void Update()
    {
        t += Time.deltaTime;
        if (t > 1f)
        {
            t = 0;
            Debug.Log("[Heartbeat] running");
        }
    }
}
