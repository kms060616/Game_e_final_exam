using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SmeltingMiniGame : MonoBehaviour
{
    [Header("UI")]
    public Slider tempSlider;
    public Text infoText;
    public Text countText;

    public Image successZone;

    [Header("게이지 설정")]
    public float gaugeSpeed = 1.5f;   // 게이지 움직이는 속도
    [Range(0f, 1f)] public float successMin = 0.4f; // 적정 온도 구간 시작
    [Range(0f, 1f)] public float successMax = 0.6f; // 적정 온도 구간 끝

    [Header("성공/실패 조건")]
    public int requiredSuccess = 3;   // 몇 번 맞추면 완성?
    public int allowedFail = 3;       // 몇 번 틀리면 실패?

    private int successCount = 0;
    private int failCount = 0;

    private bool isRunning = false;
    private float timeOffset;

   
    private Inventory ownerInventory;
    private BlockType outputType;

    void OnEnable()
    {
        tempSlider.value = 0f;
        successCount = 0;
        failCount = 0;
        timeOffset = Random.value * 100f;

        UpdateCountText();
        if (infoText != null)
            infoText.text = "게이지가 노란 구간에 있을 때 클릭하세요!";

        //  노란 구간 위치/크기 설정
        if (successZone != null)
        {
            var rt = successZone.rectTransform;
            rt.anchorMin = new Vector2(successMin, 0f);
            rt.anchorMax = new Vector2(successMax, 1f);
            rt.offsetMin = Vector2.zero;
            rt.offsetMax = Vector2.zero;
        }

        isRunning = true;
    }

    void Update()
    {
        if (!isRunning) return;

        // 0~1 사이에서 PingPong
        float t = Mathf.PingPong((Time.time + timeOffset) * gaugeSpeed, 1f);
        tempSlider.value = t;

        // 클릭 입력 (마우스 왼쪽)
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    public void StartSmelting(Inventory inven, BlockType output)
    {
        ownerInventory = inven;
        outputType = output;
        gameObject.SetActive(true); // 패널 켜기 → OnEnable에서 초기화
    }

    void OnClick()
    {
        float v = tempSlider.value;

        if (v >= successMin && v <= successMax)
        {
            successCount++;
            if (infoText != null)
                infoText.text = "좋아요! 온도가 적당합니다!";
        }
        else
        {
            failCount++;
            if (infoText != null)
                infoText.text = "온도가 너무 높거나 낮아요...";
        }

        UpdateCountText();
        CheckResult();
    }

    void UpdateCountText()
    {
        if (countText != null)
            countText.text = $"성공: {successCount} / {requiredSuccess} | 실패: {failCount} / {allowedFail}";
    }

    void CheckResult()
    {
        if (successCount >= requiredSuccess)
        {
            Success();
        }
        else if (failCount >= allowedFail)
        {
            Fail();
        }
    }

    void Success()
    {
        isRunning = false;
        Debug.Log("제련 성공!");

        if (ownerInventory != null)
        {
            ownerInventory.Add(outputType, 1);   // SilverIngot 1개 지급
        }

        gameObject.SetActive(false); // 패널 닫기
    }

    void Fail()
    {
        isRunning = false;
        Debug.Log("제련 실패... 광석이 망가졌다");

        // 실패 시 광석을 돌려줄지 말지는 네 마음대로:
        // if (ownerInventory != null) ownerInventory.Add(BlockType.SilverOre, 1);

        gameObject.SetActive(false); // 패널 닫기
    }

    // 닫기 버튼에서 호출 가능 (강제 종료 같은 느낌)
    public void ForceClose()
    {
        isRunning = false;
        gameObject.SetActive(false);
    }
}
