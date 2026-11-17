using UnityEngine;

public class UI_Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _tower1Prefab;
    [SerializeField] private GameObject _tower2Prefab;
    [SerializeField] private GameObject _tower3Prefab;
    [SerializeField] private GameObject _tower4Prefab;

    private GameObject _prefabToUse;


    private void AddMapPrefabPlacerComponent(GameObject prefab)
    {
        prefab.AddComponent<MapPrefabPlacer>();
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
