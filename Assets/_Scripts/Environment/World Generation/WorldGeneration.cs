using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Basic world generation script, is not wave function collapse so may not always be possible to generate. 
/// In this case, a custom tile should be generated (un-implemented)
/// </summary>

public class WorldGeneration : MonoBehaviour
{
    public Transform worldParent;
    public GameObject[] tilePrefabs;
    public GameObject spawnerPrefab;
    public GameObject garagePrefab;
    public int viewDistance = 5;
    public float spawnerChance = 0.3f;
    public float garageChance = 0.05f;
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
                    spawnedTiles.TryGetValue(tilePos + Vector2Int.left, out Tile lN);
                    spawnedTiles.TryGetValue(tilePos + Vector2Int.right, out Tile rN);
                    spawnedTiles.TryGetValue(tilePos + Vector2Int.up, out Tile tN);
                    spawnedTiles.TryGetValue(tilePos + Vector2Int.down, out Tile bN);

                    string requiredEdges = "";
                    if (lN != null) requiredEdges += $"r:{lN.rightEdge} ";
                    if (rN != null) requiredEdges += $"l:{rN.leftEdge} ";
                    if (tN != null) requiredEdges += $"b:{tN.bottomEdge} ";
                    if (bN != null) requiredEdges += $"t:{bN.topEdge}";

                    Debug.LogError($"Could not place a tile at {tilePos} ({requiredEdges})");
                    continue;
                }

                GameObject newTileObj = Instantiate(newTilePrefab, new Vector3(tilePos.x * tileSize, tilePos.y * tileSize, 0), Quaternion.identity, worldParent);
                Tile newTile = newTileObj.GetComponent<Tile>();
                newTileObj.name = tilePos.ToString() + newTile.tileName;
                spawnedTiles.Add(tilePos, newTile);
                AddRandomTilePois(tilePos);
            }
        }
    }

    private void DeactivateOldTiles()
    {
        Vector2Int playerTilePosition = GetTilePosition(transform.position);

        List<Vector2Int> tilesToDeactivate = new List<Vector2Int>();

        // Seperated into 2 seperate loops because otherwise it can skip some elements

        foreach (var tileEntry in spawnedTiles)
            if (Vector2Int.Distance(tileEntry.Key, playerTilePosition) > viewDistance)
                tilesToDeactivate.Add(tileEntry.Key);

        foreach (var tilePos in tilesToDeactivate)
            spawnedTiles[tilePos].gameObject.SetActive(false);
    }

    private void AddRandomTilePois(Vector2Int tilePos)
    {
        if (Random.Range(0f, 1f) <= garageChance)
        {
            Vector2 pos = new Vector2(Random.Range(-tileSize/2f, tileSize/2f), Random.Range(-tileSize / 2f, tileSize / 2f)) + 32*tilePos;
            Instantiate(garagePrefab, spawnedTiles[tilePos].transform);
            // Debug.Log($"Garage spawned at {pos} (tile {tilePos})");
        }
       
        
        if (Random.Range(0f, 1f) <= spawnerChance)
        {
            Vector2 pos = new Vector2(Random.Range(-tileSize / 2f, tileSize / 2f), Random.Range(-tileSize / 2f, tileSize / 2f)) + 32*tilePos;
            EnemySpawner spawner = Instantiate(spawnerPrefab, spawnedTiles[tilePos].transform).GetComponent<EnemySpawner>();
            spawner.player = gameObject;
            // Debug.Log($"Spawner spawned at {pos} (tile {tilePos})");
        }
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
            if (tile.leftEdge != leftNeighbor.rightEdge)
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.right, out Tile rightNeighbor))
            if (tile.rightEdge != rightNeighbor.leftEdge)
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.up, out Tile topNeighbor))
            if (tile.topEdge != topNeighbor.bottomEdge)
                return false;

        if (spawnedTiles.TryGetValue(tilePos + Vector2Int.down, out Tile bottomNeighbor))
            if (tile.bottomEdge != bottomNeighbor.topEdge)
                return false;

        return true;
    }
    private Vector2Int GetTilePosition(Vector3 worldPosition)
    {
        return new Vector2Int(Mathf.RoundToInt(worldPosition.x / tileSize), 
                              Mathf.RoundToInt(worldPosition.y / tileSize));
    }
}
