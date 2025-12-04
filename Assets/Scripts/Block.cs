using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BlockType { Dirt, Grass, Water, GoldOre, GoldIngot, SilverOre, SilverIngot }
public class Block : MonoBehaviour
{
    [Header("Block stat")]

    public BlockType type = BlockType.Dirt;
    public int maxHp = 3;
    


    [HideInInspector] public int hp;

    public int dropCount = 1;
    public bool mineable = true;

    private void Awake()
    {
        hp = maxHp;
        if (GetComponent<Collider>() == null) gameObject.AddComponent<BoxCollider>();
        if (string.IsNullOrEmpty(gameObject.tag) || gameObject.tag == "Untagged")
            gameObject.tag = "Block";
    }

    public void Hit(int damage, Inventory inven)
    {
        if (!mineable) return;
        hp -= damage;
        
        if (hp <= 0)
        {
            if (inven != null && dropCount > 0)
                inven.Add(type, dropCount);

            Destroy(gameObject);


        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
