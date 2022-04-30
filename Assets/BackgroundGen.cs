using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundGen : MonoBehaviour
{
    public GameObject BackgroundTilePrefab;
    public Sprite[] BackgroundTileSprites;

    public int BackgroundTilesHeight = 100;
    public int BackgroundTilesWidth = 20;

    public int TileDespawnRadius = 21;
    public int TileSpawnRadius = 20;

    public int MaxHeight = 150;

    Transform tileConatiner;

    byte?[,] tileIDs;
    GameObject[,] activeTiles;
    List<GameObject> activeTilesList;

    private void Awake()
    {
        tileConatiner = this.transform;

        tileIDs = new byte?[BackgroundTilesWidth, BackgroundTilesHeight];
        activeTiles = new GameObject[BackgroundTilesWidth, BackgroundTilesHeight];
        activeTilesList = new List<GameObject>();

        tileIDs[BackgroundTilesWidth - 5, BackgroundTilesHeight - 3] = 0;
    }

    private void Update()
    {
        UpdateActiveTiles();
    }

    private void UpdateActiveTiles()
    {
        Vector2Int playerTileCoord = WorldCoordToTileCoord(PlayerInput.Instance.transform.position);

        List<GameObject> newActiveTilesList = new List<GameObject>();
        foreach (GameObject tileGO in activeTilesList)
        {
            Vector2Int tileCoord = WorldCoordToTileCoord(tileGO.transform.position);
            if (Mathf.Abs(tileCoord.x - playerTileCoord.x) > TileDespawnRadius || Mathf.Abs(tileCoord.y - playerTileCoord.y) > TileDespawnRadius || tileCoord.y > MaxHeight)
            {
                Destroy(tileGO);
            } else
            {
                newActiveTilesList.Add(tileGO);
            }
        }
        activeTilesList = newActiveTilesList;

        for (int x = playerTileCoord.x - TileSpawnRadius; x <= playerTileCoord.x + TileSpawnRadius; x++)
        {
            for (int y = playerTileCoord.y - TileSpawnRadius; y <= playerTileCoord.y + TileSpawnRadius; y++)
            {
                if (y > MaxHeight)
                {
                    continue;
                }
                ActivateTileAt(x, y);
            }
        }
    }

    private void ActivateTileAt(int x, int y)
    {
        if (GetTileAt(x, y) != null)
            return; //already active

        GameObject newTile = Instantiate(BackgroundTilePrefab, tileConatiner);
        newTile.GetComponent<SpriteRenderer>().sprite = BackgroundTileSprites[GetTileIdAt(x, y)];
        newTile.name = "Tile " + x.ToString() + "/" + y.ToString() + " # id: " + GetTileIdAt(x, y);
        newTile.transform.position = new Vector3(
                x,
                y,
                0
            );

        activeTiles[Modulo(x, BackgroundTilesWidth), Modulo(y, BackgroundTilesHeight)] = newTile;
        activeTilesList.Add(newTile);
    }

    private Vector2Int WorldCoordToTileCoord(Vector3 worldPos)
    {
        return new Vector2Int(
                Mathf.FloorToInt(worldPos.x),
                Mathf.FloorToInt(worldPos.y)
            );
    }

    private GameObject GetTileAt(int x, int y) 
    {
        return activeTiles[Modulo(x, BackgroundTilesWidth), Modulo(y, BackgroundTilesHeight)];
    }

    private byte GetTileIdAt(int x, int y)
    {
        return tileIDs[Modulo(x, BackgroundTilesWidth), Modulo(y, BackgroundTilesHeight)] ??= (byte)Random.Range(0, BackgroundTileSprites.Length);
    }

    private int Modulo(int total, int divider)
    {
        int r = total % divider;
        return (r < 0) ? r + divider : r;
    }
}
