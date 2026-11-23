using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class UI_Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _tower1Prefab;
    [SerializeField] private GameObject _tower2Prefab;
    [SerializeField] private GameObject _tower3Prefab;
    [SerializeField] private GameObject _tower4Prefab;

    [SerializeField] private Map _map;

    private GameObject _prefabToUse;

    //private GameObject _GOtoPlace;
    private MapPrefabPlacer _prefabPlacer;

    private void AddMapPrefabPlacerComponent(GameObject prefab)
    {
        var go = new GameObject();
        
        go.AddComponent<MapPrefabPlacer>();
        _prefabPlacer = go.GetComponent<MapPrefabPlacer>();
        print(_prefabPlacer);

        _prefabPlacer.Initialize(_map,_prefabToUse,TileType.Path);
    }

    private void Update()
    {
        if (_prefabPlacer != null)
        {
            var mousePos = Pointer.current.position.ReadValue();
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            bool hasClickMouse = InputSystem.actions.FindAction("Attack").WasPressedThisFrame();

            bool hasHit = Physics.Raycast(ray, out RaycastHit hit);
            if (hasHit)
            {
                _prefabPlacer.SetPosition(hit.point);
            }
        }
    }


    // the buttons scripts
    public void TakeTower1()
    {
        _prefabToUse = _tower1Prefab;
        AddMapPrefabPlacerComponent(_prefabToUse);
        print(_prefabToUse.name);
    }

    public void TakeTower2()
    {
        _prefabToUse = _tower2Prefab;
        AddMapPrefabPlacerComponent(_prefabToUse);
        print(_prefabToUse.name);
    }

    public void TakeTower3()
    {
        _prefabToUse = _tower3Prefab;
        AddMapPrefabPlacerComponent(_prefabToUse);
        print(_prefabToUse.name);
    }

    public void TakeTower4()
    {
        _prefabToUse = _tower4Prefab;
        AddMapPrefabPlacerComponent(_prefabToUse);
        print(_prefabToUse.name);
    }

    public void CancelUsePrefab()
    {
        _prefabToUse = null;
        print(_prefabToUse);
    }
}
