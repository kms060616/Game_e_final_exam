using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHarvester : MonoBehaviour
{
    public float rayDistance = 5f;

    public LayerMask hitMask = ~0;
    public int toolDamage = 1;
    public float hitCooldown = 0.15f;
    private float _nextHitTime;
    private Camera _cam;
    public Inventory inventory;
    InventoryUI invenUI;

    public GameObject seletedBlock;

    private void Awake()
    {
        

        inventory = GetComponent<Inventory>();   //같은 플레이어 오브젝트에 붙은 Inventory만 사용
        invenUI = FindObjectOfType<InventoryUI>(true);
        Debug.Log($"[Harvester] invenUI={(invenUI ? invenUI.gameObject.name : "NULL")}");
        _cam = Camera.main;
        

    }
    // Start is called before the first frame update
    void Start()
    {
        if (invenUI == null) invenUI = FindObjectOfType<InventoryUI>(true);
        if (_cam == null) _cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        bool hasPreview = (seletedBlock != null);

        // 1) 선택 안 했을 때(채집 모드)
        if (invenUI.selectedIndex < 0)
        {
            if (hasPreview) seletedBlock.transform.localScale = Vector3.zero;

            if (Input.GetMouseButton(0) && Time.time >= _nextHitTime)
            {
                _nextHitTime = Time.time + hitCooldown;

                Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
                if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
                {
                    var block = hit.collider.GetComponent<Block>();
                    if (block != null)
                    {
                        block.Hit(toolDamage, inventory);
                    }
                }
            }

            return; // 채집 모드 끝
        }

        // 2) 선택 했을 때(배치 모드)
        Ray rayDebug = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(rayDebug, out var hitDebug, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
        {
            Vector3Int placePos = AdjacentCellOnHitFace(hitDebug);

            if (hasPreview)
            {
                seletedBlock.transform.localScale = Vector3.one;
                seletedBlock.transform.position = placePos;
                seletedBlock.transform.rotation = Quaternion.identity;
            }
        }
        else
        {
            if (hasPreview) seletedBlock.transform.localScale = Vector3.zero;
            Debug.Log("레이캐스트가 아무것도 못 맞췄어요!");
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask, QueryTriggerInteraction.Ignore))
            {
                Vector3Int placePos = AdjacentCellOnHitFace(hit);

                BlockType selected = invenUI.GetInventorySlot();

                Debug.Log($"[Place] selected={selected} have={inventory.GetCount(selected)} invObj={inventory.gameObject.name}");

                if (inventory.Consume(selected, 1))
                {
                    Debug.Log("[Place] consume OK");
                    var map = FindAnyObjectByType<NoiseVoxelMap>();
                    Debug.Log($"[Place] map={(map ? map.name : "NULL")} pos={placePos}");
                    if (map != null) map.PlaceTile(placePos, selected);
                }
                else
                {
                    Debug.Log("[Place] consume FAIL");
                }
            }
        }
    }

    static Vector3Int AdjacentCellOnHitFace(in RaycastHit hit)
    {
        Vector3 baseCenter = hit.collider.transform.position;
        Vector3 adjCentar = baseCenter + hit.normal;
        return Vector3Int.RoundToInt(adjCentar);
    }
}
