using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Basic world generation script, is not wave function collapse so may not always be possible to generate. 
/// In this case, a custom tile should be generated (un-implemented)
/// </summary>

public class WorldGeneration : MonoBehaviour
{
    public Transform worldParent;
    public GameObject[] tilePrefabs;
    public int viewDistance = 5;
    public float tileSize;
    private Dictionary<Vector2Int, Tile> spawnedTiles = new Dictionary<Vector2Int, Tile>();


    private void Update()
    {
        GenerateTiles();
        DeactivateOldTiles();
    }

    private void GenerateTiles()
    {
        Vector2Int playerTilePosition = GetTilePosition(transform.position);

        for (int x = -viewDistance; x <= viewDistance; x++)
        {
            for (int y = -viewDistance; y <= viewDistance; y++)
            {
                Vector2Int tilePos = new Vector2Int(playerTilePosition.x + x, playerTilePosition.y + y);
                if (Vector2Int.Distance(tilePos, playerTilePosition) > viewDistance) continue;

                if (spawnedTiles.TryGetValue(tilePos, out Tile tile))
                {
                    tile.gameObject.SetActive(true);
                    continue;
                }

                GameObject newTilePrefab = GetRandomValidTile(tilePos);
                if (newTilePrefab == null)
                {
                    Debug.LogError($"Could not place a tile at {tilePos}");
                    continue;
                }

                GameObject newTileObj = Instantiate(newTilePrefab, new Vector3(tilePos.x * tileSize, tilePos.y * tileSize, 0), Quaternion.identity, worldParent);
                Tile newTile = newTileObj.GetComponent<Tile>();
                newTileObj.name = tilePos.ToString() + newTile.tileName;
                spawnedTiles.Add(tilePos, newTile);
            }
        }
    }

    private void DeactivateOldTiles()
    {
        Vector2Int playerTilePosition = GetTilePosition(transform.position);

        List<Vector2Int> tilesToDeactivate = new List<Vector2Int>();

        foreach (var tileEntry in spawnedTiles)
            if (Vector2Int.Distance(tileEntry.Key, playerTilePosition) > viewDistance)
                tilesToDeactivate.Add(tileEntry.Key);

        foreach (var tilePos in tilesToDeactivate)
            spawnedTiles[tilePos].gameObject.SetActive(false);
    }
    private GameObject GetRandomValidTile(Vector2Int tilePos)
    {
        List<GameObject> validTiles = GetValidTiles(tilePos);
        if (validTiles.Count > 0)
            return validTiles[Random.Range(0, validTiles.Count)];

        return null;
    }

    private List<GameObject> GetValidTiles(Vector2Int tilePos)
    {
        List<GameObject> validTiles = new List<GameObject>();

        foreach (GameObject tilePrefab in tilePrefabs)
        {
            Tile tile = tilePrefab.GetComponent<Tile>();
            if (TileEdgesMatch(tile, tilePos))
                validTiles.Add(tilePrefab);
        }

        return validTiles;
    }

    private bool TileEdgesMatch(Tile tile, Vector2Int tilePos)
    {
        // Check all four neighbors (left, right, up, down) for edge compatibility
        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.left, out Tile leftNeighbor))
            if (tile.GetEdgeType(Tile.Edge.left) != leftNeighbor.GetEdgeType(Tile.Edge.right))
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.right, out Tile rightNeighbor))
            if (tile.GetEdgeType(Tile.Edge.right) != rightNeighbor.GetEdgeType(Tile.Edge.left))
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.up, out Tile topNeighbor))
            if (tile.GetEdgeType(Tile.Edge.top) != topNeighbor.GetEdgeType(Tile.Edge.bottom))
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.down, out Tile bottomNeighbor))
            if (tile.GetEdgeType(Tile.Edge.bottom) != bottomNeighbor.GetEdgeType(Tile.Edge.top))
                return false;

        return true;
    }
    private Vector2Int GetTilePosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x / tileSize), 
                              Mathf.RoundToInt(worldPosition.y / tileSize));
    }
}
