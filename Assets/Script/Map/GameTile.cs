using System;
using UnityEngine;
using UnityEngine.Tilemaps;

[Flags]
public enum TileType
{
    Air = 0, // Useful when checking a tile that may not exist.
    Grass = 1 << 0,
    Path = 1 << 1,
}

[CreateAssetMenu(fileName = "NewGameTile", menuName = "2D/Tiles/Game Tile")]
public class GameTile : Tile
{
    public TileType type;
}
