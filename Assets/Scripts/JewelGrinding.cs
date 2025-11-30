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
    public float progressSpeed = 4f;
    public float crackSpeed = 3f; 

    private Vector3 lastMousePos;
    private bool isGrinding = false;


    void Start()
    {
        StartGrinding();
    }
    void Update()
    {
        if (!isGrinding) return;

        if (Input.GetMouseButton(0))
        {
            Vector3 current = Input.mousePosition;

            float dragSpeed = (current - lastMousePos).magnitude / Time.deltaTime;

            // 연마 휠 회전
            float rotateAmount = dragSpeed * 0.02f;
            wheelImage.transform.Rotate(0, 0, -rotateAmount);

            // 적정 속도 체크
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

    public void StartGrinding()
    {
        isGrinding = true;
        lastMousePos = Input.mousePosition;
        progressBar.value = 0;
        crackBar.value = 0;
    }

    void CheckResult()
    {
        if (progressBar.value >= 1f)
        {
            Success();
        }
        else if (crackBar.value >= 1f)
        {
            Fail();
        }
    }

    void Success()
    {
        isGrinding = false;
        Debug.Log("연마 성공!");
        // TODO: 아이템 반환 + UI 닫기
    }

    void Fail()
    {
        isGrinding = false;
        Debug.Log("보석 깨짐…");
        // TODO: 실패 처리
    }
}
