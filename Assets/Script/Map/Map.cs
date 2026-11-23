using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private Dictionary<Vector3Int, bool> _occupiedDict = new Dictionary<Vector3Int, bool>();

    public Vector3Int GetCellPosition(Vector3 worldPosition)
    {
        return tilemap.WorldToCell(worldPosition);
    }

    public Vector3 GetCellCenterWorld(Vector3Int cellPosition)
    {
        return tilemap.GetCellCenterWorld(cellPosition);
    }

    public TileType GetTileTypeAtWorldPos(Vector3 worldPosition)
    {
        return GetTileTypeAtCell(GetCellPosition(worldPosition));
    }

    public TileType GetTileTypeAtCell(Vector3Int cellPosition)
    {
        GameTile tile = tilemap.GetTile<GameTile>(cellPosition);

        if (tile == null)
            return TileType.Air;

        return tile.type;
    }

    public bool IsOccupied(Vector3Int cellPosition)
    {
        return _occupiedDict.TryGetValue(cellPosition, out bool occupied) && occupied;
    }

    public void SetOccupied(Vector3Int cellPosition, bool occupied)
    {
        if (occupied)
        {
            _occupiedDict[cellPosition] = true;
        }
        else
        {
            _occupiedDict.Remove(cellPosition);
        }
    }
}
