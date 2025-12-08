using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum AccessoryType
{
    GoldRing,
    GoldNecklace,
    SilverRing,
    SilverNecklace,
    Dirt
}

[CreateAssetMenu(fileName = "AccessoryData", menuName = "MiningGame/AccessoryData")]
[System.Serializable]
public class AccessoryData : ScriptableObject
{
    [Header("기본 정보")]
    public AccessoryType type;

    [Header("필요 재료")]
    public BlockType requiredMaterial; 
    public int requiredCount = 1;

    [Header("퍼즐 이미지")]
    public Sprite puzzleSprite;

    [Header("퍼즐 정답 회전값 (조각 개수만큼)")]
    public int[] correctRotations;

    [Header("완성 결과 아이템")]
    public BlockType resultItem;
}
