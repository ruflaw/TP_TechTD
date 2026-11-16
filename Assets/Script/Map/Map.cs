using UnityEngine;
using UnityEngine.Tilemaps;

public class Map : MonoBehaviour
{
    [SerializeField] private Tilemap tilemap;

    public TileType GetTileTypeAtWorldPos(Vector3 worldPosition)
    {
        Vector3Int cellPos = tilemap.WorldToCell(worldPosition);
        return GetTileTypeAtCell(cellPos);
    }

    TileType GetTileTypeAtCell(Vector3Int cellPosition)
    {
        GameTile tile = tilemap.GetTile<GameTile>(cellPosition);
        if (tile == null)
        {
            return TileType.Air;
        }

        return tile.type;        
    }
    
    private void Update()
    {
        var coords = new Vector3Int(0, 0, 0);
        print(GetTileTypeAtWorldPos(coords));        
    }
}
