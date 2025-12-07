using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngravePuzzleManager : MonoBehaviour
{
    public EngravePiece[] pieces;
    public Text resultText;

    private Inventory ownerInventory;
    private BlockType outputType;

    void OnEnable()
    {
        resultText.text = "문양을 완성하세요!";
    }

    public void StartEngraving(Inventory inven, BlockType output)
    {
        ownerInventory = inven;
        outputType = output;

        gameObject.SetActive(true);
    }

    public void CheckPuzzle()
    {
        foreach (var piece in pieces)
        {
            if (!piece.IsCorrect())
                return;
        }

        
        Success();
    }

    void Success()
    {
        resultText.text = "세공 성공!";

        if (ownerInventory != null)
            ownerInventory.Add(outputType, 1);   // 악세서리 완성!

        Invoke(nameof(ClosePanel), 1.2f);
    }

    void ClosePanel()
    {
        gameObject.SetActive(false);
    }
}
