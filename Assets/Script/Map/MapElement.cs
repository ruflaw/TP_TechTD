using UnityEngine;

public class MapElement : MonoBehaviour
{
    [SerializeField] private Vector2Int size = Vector2Int.one;

    private Map _map;
    private Vector3Int _origin;
    public Vector2Int Size => size;

    /// <summary>
    /// Initializes this element and registers its occupied cells.
    /// </summary>
    public void Initialize(Map map, Vector3Int origin)
    {
        _map = map;
        _origin = origin;

        SetOccupied(true);
    }

    private void SetOccupied(bool occupied)
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Vector3Int cell = _origin + new Vector3Int(x, y, 0);
                _map.SetOccupied(cell, occupied);
            }
        }
    }

    private void OnDestroy()
    {
        if (_map == null)
            return;

        SetOccupied(false);
    }
}
