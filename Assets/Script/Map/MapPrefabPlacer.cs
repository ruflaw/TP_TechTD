using System.Security.Cryptography;
using UnityEngine;

public class MapPrefabPlacer : MonoBehaviour
{
    private GameObject _prefab;
    private Map _map;

    private GameObject _instance;
    private MapElement _instanceMapElement;

    private TileType _tileType;
    private bool _canPlace;
    private int _rotation;

    public void Initialize(Map map, GameObject prefab, TileType tileType)
    {
        this._map = map;
        this._prefab = prefab;
        this._tileType = tileType;
 
        _instance = Instantiate(prefab, transform);
        _instance.SetActive(false);
        _instanceMapElement = _instance.GetComponent<MapElement>();
    }

    /// <summary>
    /// Snaps the prefab to the grid and checks whether it can be placed.
    /// Returns true if placement is valid.
    /// </summary>
    public bool SetPosition(Vector3 worldPosition)
    {
        Vector3Int cell = _map.GetCellPosition(worldPosition);

        // Snap to the center of the grid cell.
        Vector3 snappedPosition = _map.GetCellCenterWorld(cell);

        _instance.transform.position = snappedPosition;
        _instance.SetActive(true);

        _canPlace = CanPlaceAt(cell);

        return _canPlace;
    }

    private bool CanPlaceAt(Vector3Int origin)
    {
        Vector2Int size = GetRotatedSize();

        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                TileType cellTileType = _map.GetTileTypeAtCell(cell);
                if (cellTileType != _tileType)
                {
                    print("mauvaiseTile");
                    return false;
                }

                // Check whether the tile is already occupied.
                if (_map.IsOccupied(cell))
                {
                    print("occupe");
                    return false;
                }
            }
        }

        return true;
    }

    private Vector2Int GetRotatedSize()
    {
        // A 90 or 270 degree rotation swaps width and height.
        if (_rotation % 2 != 0)
        {
            return new Vector2Int(
                _instanceMapElement.Size.y,
                _instanceMapElement.Size.x
            );
        }

        return _instanceMapElement.Size;
    }

    /// <summary>
    /// Rotates the prefab in a direction.
    /// Can be either 1 or -1. Will rotate +/-90 degrees.
    /// </summary>
    public void Rotate(int direction)
    {
        if (direction != 1 && direction != -1)
            return;

        _rotation = (_rotation + direction + 4) % 4;

        _instance.transform.Rotate(0f, 0f, direction * 90f);
    }

    public void Confirm()
    {
        if (!_canPlace)
        {
            print("Can'tPlace");
            return;
        }

        Vector3Int origin = _map.GetCellPosition(_instance.transform.position);

        if (_instanceMapElement == null)
        {
            print("Prefab is missing a MapElement component!");
            return;
        }

        _instance.transform.SetParent(null);
        _instanceMapElement.Initialize(_map, origin);

        _instance = null;
    }
}
