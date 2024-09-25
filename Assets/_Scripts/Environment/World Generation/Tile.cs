using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public enum Edge { top, bottom, left, right }
    public enum EdgeType { desert, wideRoad, city, water, ERROR }

    public EdgeType topEdge, bottomEdge, leftEdge, rightEdge;


    public EdgeType GetEdgeType(Edge edge)
    {
        switch(edge)
        {
            case Edge.top:
                return topEdge;
            case Edge.bottom:
                return bottomEdge;
            case Edge.left:
                return leftEdge;
            case Edge.right:
                return rightEdge;
            default:
                return EdgeType.ERROR;
        }
    }
}