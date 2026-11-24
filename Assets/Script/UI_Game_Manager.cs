using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class UI_Game_Manager : MonoBehaviour
{
    [SerializeField] private GameObject _tower1Prefab;
    [SerializeField] private GameObject _tower2Prefab;
    [SerializeField] private GameObject _tower3Prefab;
    [SerializeField] private GameObject _tower4Prefab;

    [SerializeField] private Map _map;

    [SerializeField] private Image _healthBarFilling;
    [SerializeField] private TextMeshProUGUI _currencyText;

    [SerializeField] private GameObject world;

    private GameObject _prefabToUse;
    private MapElement _currentMapElement;

    private void CreatePrefabInstance()
    {
        if (!_prefabToUse) return;

        var go = Instantiate(_prefabToUse);
        _currentMapElement = go.GetComponent<MapElement>();
        _currentMapElement.Initialize(_map);
        if (go.gameObject.TryGetComponent<Generator>(out var generator))
        {
            generator.enabled = true;
            generator.AddWorldRef(world);
        }
    }

    private void Update()
    {

        var mousePos = Pointer.current.position.ReadValue();
        Ray ray = Camera.main.ScreenPointToRay(mousePos);

        bool hasClickMouse = InputSystem.actions.FindAction("Attack").WasPressedThisFrame();
        bool hasInteracted = InputSystem.actions.FindAction("Interact").WasPressedThisFrame();

        bool hasHit = Physics.Raycast(ray, out RaycastHit hit, 5000f, Physics.AllLayers, QueryTriggerInteraction.Ignore);
        if (hasHit)
        {
            if ( _currentMapElement != null)
            {
                _currentMapElement.SetPosition(hit.point);

                if (hasClickMouse)
                {
                    if (_currentMapElement.TryConfirmPlacement())
                    {
                        _currentMapElement = null;   
                    }
                }
                if (hasInteracted)
                {
                    _currentMapElement.Rotate();
                }
            }
            else if (_currentMapElement == null)
            {
                var cellPos = _map.GetCellPosition(hit.point);
                var cellObject = _map.GetCellOccupiedObject(cellPos);
                if (cellObject != null && hasClickMouse)
                {
                    Destroy(cellObject);
                }
            }
        }
    }

    // the buttons scripts
    public void TakeTower1()
    {
        _prefabToUse = _tower1Prefab;
        CreatePrefabInstance();
        print(_prefabToUse.name);
    }

    public void TakeTower2()
    {
        _prefabToUse = _tower2Prefab;
        CreatePrefabInstance();
        print(_prefabToUse.name);
    }

    public void TakeTower3()
    {
        _prefabToUse = _tower3Prefab;
        CreatePrefabInstance();
        print(_prefabToUse.name);
    }

    public void TakeTower4()
    {
        _prefabToUse = _tower4Prefab;
        CreatePrefabInstance();
        print(_prefabToUse.name);
    }

    public void CancelUsePrefab()
    {
        _prefabToUse = null;
        print(_prefabToUse);
        if (_currentMapElement  != null)
        {
            Destroy(_currentMapElement.gameObject);
        }
    }

    public void UpdateHealthPercent(float percent)
    {
        _healthBarFilling.fillAmount = percent;
    }

    public void UpdateCurrencyAmount(int CurrentCurrency)
    {
        _currencyText.text = CurrentCurrency.ToString();
    }
}
