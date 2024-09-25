using System.Collections.Generic;
using UnityEngine;

public class TileData : MonoBehaviour
{
    public TileEdgeTypes[] edgeTypes;
    public Dictionary<TileEdges, TileEdgeTypes> edges = new Dictionary<TileEdges, TileEdgeTypes>();

    private void Awake()
    {
        edges.Add(TileEdges.up, edgeTypes[0]);
        edges.Add(TileEdges.right, edgeTypes[1]);
        edges.Add(TileEdges.down, edgeTypes[2]);
        edges.Add(TileEdges.left, edgeTypes[3]);
    }
}

public enum TileEdges { up, down, left, right }
public enum TileEdgeTypes { desert, wideRoad, city, water }