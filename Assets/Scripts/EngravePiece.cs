using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EngravePiece : MonoBehaviour
{
    public int correctRotation;   // 정답 각도 (0, 90, 180, 270)
    public int currentRotation;

    private RectTransform rt;

    void Awake()
    {
        rt = GetComponent<RectTransform>();
    }

    void Start()
    {
        // 시작할 때 랜덤 회전
        int[] rots = { 0, 90, 180, 270 };
        currentRotation = rots[Random.Range(0, rots.Length)];
        rt.localRotation = Quaternion.Euler(0, 0, currentRotation);

        GetComponent<Button>().onClick.AddListener(Rotate);
    }

    void Rotate()
    {
        currentRotation = (currentRotation + 90) % 360;
        rt.localRotation = Quaternion.Euler(0, 0, currentRotation);
        FindObjectOfType<EngravePuzzleManager>().CheckPuzzle();

    }

    public bool IsCorrect()
    {
        return currentRotation == correctRotation;
    }
}
