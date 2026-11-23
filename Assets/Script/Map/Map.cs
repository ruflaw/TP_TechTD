using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    private Dictionary<Vector3Int, GameObject> _occupiedDict = new Dictionary<Vector3Int, GameObject>();

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

    public GameObject GetCellOccupiedObject(Vector3Int cellPosition)
    {
        var obj = _occupiedDict.TryGetValue(cellPosition, out GameObject inst);
        return obj ? inst : null;
    }

    public void SetOccupied(Vector3Int cellPosition, GameObject inst)
    {
        if (inst)
        {
            _occupiedDict[cellPosition] = inst;
        }
        else
        {
            _occupiedDict.Remove(cellPosition);
        }
    }
}
