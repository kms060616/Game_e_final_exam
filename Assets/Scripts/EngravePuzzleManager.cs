using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EngravePuzzleManager : MonoBehaviour
{
    public EngravePiece[] pieces;   // 9개
    public Text resultText;

    private Inventory ownerInventory;
    private AccessoryData currentData;

    public void StartEngraving(Inventory inven, AccessoryData data)
    {
        ownerInventory = inven;
        currentData = data;

        Sprite[] sliced = SliceSprite(data.puzzleSprite, 3, 3);

        for (int i = 0; i < pieces.Length; i++)
        {
            // 이전 상태 제거
            pieces[i].ResetRotation();

            //정답 설정
            pieces[i].correctRotation = data.correctRotations[i];

            // 한 번만 랜덤 섞기
            pieces[i].Randomize();

            // 퍼즐 이미지 적용
            Image img = pieces[i].GetComponent<Image>();
            if (img != null && i < sliced.Length)
                img.sprite = sliced[i];
        }

        resultText.text = "문양을 완성하세요!";
        gameObject.SetActive(true);
    }

    Sprite[] SliceSprite(Sprite source, int col, int row)
    {
        if (source == null)
        {
            Debug.LogError("퍼즐 스프라이트가 비어 있습니다!");
            return null;
        }

        Texture2D tex = source.texture;
        int w = tex.width / col;
        int h = tex.height / row;

        Sprite[] result = new Sprite[col * row];
        int index = 0;

        //UI 그리드 순서: 왼쪽 → 오른쪽, 위 → 아래
        for (int y = 0; y < row; y++)
        {
            for (int x = 0; x < col; x++)
            {
                int realY = row - 1 - y;  //텍스처 좌표 보정
                Rect r = new Rect(x * w, realY * h, w, h);
                result[index++] = Sprite.Create(
                    tex,
                    r,
                    new Vector2(0.5f, 0.5f)
                );
            }
        }

        return result;
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
        ownerInventory.Add(currentData.resultItem, 1);
        Invoke(nameof(Close), 1.2f);
    }

    void Close()
    {
        gameObject.SetActive(false);
    }
}
