using UnityEngine;
using UnityEngine.Tilemaps;

public enum TileType
{
    Air, // Useful when checking a tile that may not exist.
    Grass,
    Path,
}

[CreateAssetMenu(fileName = "NewGameTile", menuName = "2D/Tiles/Game Tile")]
public class GameTile : Tile
{
    public TileType type;
    public bool occupied = true;
}
