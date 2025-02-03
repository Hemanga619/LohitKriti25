using UnityEngine;

public class Tile : MonoBehaviour
{
    private int x, y;
    private SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void InitTile(int xPos, int yPos)
    {
        x = xPos;
        y = yPos;
    }

    private void OnMouseDown()
    {
        Debug.Log($"Tile clicked: ({x}, {y})");
        // Handle tile swapping logic here
    }
}