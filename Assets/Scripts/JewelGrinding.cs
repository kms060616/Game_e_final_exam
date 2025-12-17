using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JewelGrinding : MonoBehaviour
{
    public Image wheelImage;
    public Slider progressBar;
    public Slider crackBar;

    public float minSpeed = 200f;
    public float maxSpeed = 800f;
    public float progressSpeed = 5f;
    public float crackSpeed = 1f;

    private Vector3 lastMousePos;
    private bool isGrinding = false;

    
    private Inventory ownerInventory;
    private BlockType outputType;

    void Update()
    {
        if (!isGrinding) return;

        if (Input.GetMouseButton(0))
        {
            Vector3 current = Input.mousePosition;
            float dragSpeed = (current - lastMousePos).magnitude / Time.deltaTime;

            float rotateAmount = dragSpeed * 0.02f;
            wheelImage.transform.Rotate(0, 0, -rotateAmount);

            if (dragSpeed > minSpeed && dragSpeed < maxSpeed)
            {
                progressBar.value += progressSpeed * Time.deltaTime;
            }
            else
            {
                crackBar.value += crackSpeed * Time.deltaTime;
            }

            lastMousePos = current;
        }

        CheckResult();
    }

    
    public void StartGrinding(Inventory inven, BlockType output)
    {
        ownerInventory = inven;
        outputType = output;

        isGrinding = true;
        lastMousePos = Input.mousePosition;
        progressBar.value = 0;
        crackBar.value = 0;
    }

    void CheckResult()
    {
        if (progressBar.value >= 1f)
            Success();
        else if (crackBar.value >= 1f)
            Fail();
    }

    void Success()
    {
        isGrinding = false;
        Debug.Log("연마 성공!");

        //연마 성공 시: 인벤토리에 결과 아이템 1개 지급
        if (ownerInventory != null)
        {
            ownerInventory.Add(outputType, 1);
        }

        gameObject.SetActive(false); // 패널 끄기
    }

    void Fail()
    {
        isGrinding = false;
        Debug.Log("보석 깨짐…");

        // 여기서 실패 시 원석을 돌려줄지, 그냥 날려버릴지 선택 가능
        // if (ownerInventory != null) ownerInventory.Add(BlockType.GoldOre, 1);

        gameObject.SetActive(false); // 패널 끄기
    }
}
