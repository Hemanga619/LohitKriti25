using System.Collections;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int width, height;
    public GameObject tilePrefab;
    private Tile[,] gridTiles;

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        gridTiles = new Tile[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject tile = Instantiate(tilePrefab, new Vector2(x, y), Quaternion.identity);
                tile.transform.parent = transform;
                gridTiles[x, y] = tile.GetComponent<Tile>();
                gridTiles[x, y].InitTile(x, y);
            }
        }
    }
}