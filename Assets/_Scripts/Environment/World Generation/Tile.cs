using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Edge { top, bottom, left, right }
    public enum EdgeType { desert, wideRoad, city, water, ERROR }

    public EdgeType topEdge, bottomEdge, leftEdge, rightEdge;

    public string tileName = "";
}