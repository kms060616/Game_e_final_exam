using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class NoiseVoxelMap : MonoBehaviour
{
    public GameObject blockPrefabs;

    public GameObject blockWater;

    public GameObject blockGrass;

    public int width = 10;

    public int depth = 10;

    public int waterLevel = 4;

    public int maxHeight = 16;

    [SerializeField] float noiseScale = 20;
    // Start is called before the first frame update
    void Start()
    {
        float offsetX = Random.Range(-9999f, 9999f);
        float offsetZ = Random.Range(-9999f, 9999f);

        for (int x = 0; x < width; x++)      
        {
            for (int z = 0; z < depth; z++)
            {
                float nx = (x + offsetX) / noiseScale;
                float nz = (z + offsetZ) / noiseScale;

                float noise = Mathf.PerlinNoise(nx, nz);

                
                int h = Mathf.FloorToInt(noise * maxHeight);

                if (h <= 0) continue;

                
                for (int y = 0; y <= h; y++)
                {
                    if (y == h)
                        PlaceGrass(x, y, z);
                    else
                        PlaceDirt(x, y, z);
                }

                
                for (int y2 = h + 1; y2 <= waterLevel; y2++)
                {
                    PlaceWater(x, y2, z);
                }
            }
        }


    }
    public void PlaceTile(Vector3Int pos, BlockType type)
    {
        switch (type)
        {
            case BlockType.Dirt:
                PlaceDirt(pos.x, pos.y, pos.z);
                break;
            case BlockType.Grass:
                PlaceGrass(pos.x, pos.y, pos.z);
                break;
            case BlockType.Water:
                PlaceWater(pos.x, pos.y, pos.z);
                break;
        }
    }

    

    // Update is called once per frame
    void Update()
    {
        
    }

    private void  PlaceDirt(int x, int y, int z)
    {
        var go = Instantiate(blockPrefabs, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"B_(x)_(y)_(z)";
        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Dirt;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = true;

    }

    private void PlaceGrass(int x, int y, int z)
    {
        var go = Instantiate(blockGrass, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"BG_(x)_(y)_(z)";
        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Grass;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = true;
    }

    private void PlaceWater(int x, int y, int z)
    {
        var go = Instantiate(blockWater, new Vector3(x, y, z), Quaternion.identity, transform);
        go.name = $"BG_(x)_(y)_(z)";
        var b = go.GetComponent<Block>() ?? go.AddComponent<Block>();
        b.type = BlockType.Water;
        b.maxHp = 3;
        b.dropCount = 1;
        b.mineable = false;
        
    }

}
