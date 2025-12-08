using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EngravePiece : MonoBehaviour
{
    public int correctRotation;
    public int currentRotation;

    private RectTransform rt;
    private Button btn;

    void Awake()
    {
        Init();
    }

    //  rt, btn이 없으면 언제든 다시 잡도록 하는 안전 초기화
    void Init()
    {
        if (rt == null)
            rt = GetComponent<RectTransform>();

        if (btn == null)
        {
            btn = GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(Rotate);
        }
    }

    public void ResetRotation()
    {
        Init();  //wake 이전 호출되어도 여기서 다시 잡힘

        currentRotation = 0;

        if (rt != null)
            rt.localRotation = Quaternion.identity;
        else
            Debug.LogError($"{gameObject.name} 에 RectTransform이 없습니다!");
    }

    public void Randomize()
    {
        Init();  

        int[] rots = { 0, 90, 180, 270 };
        currentRotation = rots[Random.Range(0, rots.Length)];

        if (rt != null)
            rt.localRotation = Quaternion.Euler(0, 0, currentRotation);
    }

    void Rotate()
    {
        Init();  

        currentRotation = (currentRotation + 90) % 360;

        if (rt != null)
            rt.localRotation = Quaternion.Euler(0, 0, currentRotation);

        var manager = FindObjectOfType<EngravePuzzleManager>();
        if (manager != null)
            manager.CheckPuzzle();
    }

    public bool IsCorrect()
    {
        return currentRotation == correctRotation;
    }
}
