using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration : MonoBehaviour
{
    public GameObject[] tiles;
    private List<TileData> tileDatas;

    public int viewDistance = 5;

    void Awake()
    {
        foreach (var tile in tiles)
            tileDatas.Add(tile.GetComponent<TileData>());
    }
}
