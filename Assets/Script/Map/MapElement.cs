using UnityEngine;

public class MapElement : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.one;
    [SerializeField] private TileType _placeableOnTileType = TileType.Grass;

    private Map _map;
    private Vector3Int _origin;

    private int _rotation; // 0 = 0°, 1 = 90°, 2 = 180°, 3 = 270°
    private bool _canPlace;
    private bool _isPlaced;

    public Vector2Int Size => size;
    public bool CanPlace => _canPlace;
    public bool IsPlaced => _isPlaced;

    /// <summary>
    /// Initializes the element for placement previewing.
    /// </summary>
    public void Initialize(Map map)
    {
        _map = map;
        _isPlaced = false;
    }

    /// <summary>
    /// Snaps the element to the grid and checks whether it can be placed.
    /// Returns true if placement is valid.
    /// </summary>
    public bool SetPosition(Vector3 worldPosition)
    {
        if (_isPlaced || _map == null)
            return false;

        _origin = _map.GetCellPosition(worldPosition);
        UpdateVisualTransform();
        _canPlace = CanPlaceAt(_origin);

        return _canPlace;
    }

    /// <summary>
    /// Rotates the element 90 degrees clockwise and re-evaluates placement.
    /// </summary>
    public void Rotate()
    {
        if (_isPlaced)
            return;

        _rotation = (_rotation + 1) % 4;
        UpdateVisualTransform();
        _canPlace = CanPlaceAt(_origin);
    }

    /// <summary>
    /// Positions and rotates the transform so the mesh stays anchored to the bottom-left (_origin) cell
    /// while expanding toward +X and +Z.
    /// </summary>
    private void UpdateVisualTransform()
    {
        if (_map == null)
            return;

        transform.rotation = Quaternion.Euler(0f, 90f * _rotation, 0f);

        float cellSize = 1f; 
        float halfCell = cellSize * 0.5f;

        // Shift from the center of the origin tile to its bottom-left (0,0,0) corner
        Vector3 bottomLeftCorner = _map.GetCellCenterWorld(_origin) - new Vector3(halfCell, 0f, halfCell);
        Vector3 offset = Vector3.zero;

        switch (_rotation)
        {
            case 0: 
                offset = Vector3.zero;
                break;
            case 1: 
                offset = new Vector3(0f, 0f, size.x * cellSize);
                break;
            case 2: 
                offset = new Vector3(size.x * cellSize, 0f, size.y * cellSize);
                break;
            case 3: 
                offset = new Vector3(size.y * cellSize, 0f, 0f);
                break;
        }

        transform.position = bottomLeftCorner + offset;
    }

    /// <summary>
    /// Locks the element into place and marks its grid cells as occupied.
    /// </summary>
    public bool TryConfirmPlacement()
    {
        if (_isPlaced || !_canPlace)
            return false;

        _isPlaced = true;
        SetOccupied(true);
        return true;
    }

    private bool CanPlaceAt(Vector3Int origin)
    {
        Vector2Int rotatedSize = GetRotatedSize();

        for (int x = 0; x < rotatedSize.x; x++)
        {
            for (int y = 0; y < rotatedSize.y; y++)
            {
                Vector3Int cell = origin + new Vector3Int(x, y, 0);

                if ((_map.GetTileTypeAtCell(cell) & _placeableOnTileType) == 0)
                    return false;

                if (_map.GetCellOccupiedObject(cell) != null)
                    return false;
            }
        }

        return true;
    }

    private void SetOccupied(bool occupied)
    {
        Vector2Int rotatedSize = GetRotatedSize();

        for (int x = 0; x < rotatedSize.x; x++)
        {
            for (int y = 0; y < rotatedSize.y; y++)
            {
                Vector3Int cell = _origin + new Vector3Int(x, y, 0);
                _map.SetOccupied(cell, occupied ? gameObject : null);
            }
        }
    }

    public Vector2Int GetRotatedSize()
    {
        if (_rotation % 2 != 0)
            return new Vector2Int(size.y, size.x);

        return size;
    }

    private void OnDestroy()
    {
        if (_map != null && _isPlaced)
        {
            SetOccupied(false);
        }
    }
}
