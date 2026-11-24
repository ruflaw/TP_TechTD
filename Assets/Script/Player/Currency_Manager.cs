using UnityEngine;

public class Currency_Manager : MonoBehaviour
{
    [SerializeField] private UI_Game_Manager _MainUI;
    [SerializeField] private int _startingCurrency;
    private int _currentCurrency;


    private void Start()
    {
        _currentCurrency = _startingCurrency;
        _MainUI.UpdateCurrencyAmount(_currentCurrency);
    }

    public int GetCurrency()
    {
        return _currentCurrency;
    }

    public void AddCurrency(int toAdd)
    {
        _currentCurrency += toAdd;
        _MainUI.UpdateCurrencyAmount(_currentCurrency);
    }

    public void RemoveCurrency(int toRemove)
    {
        _currentCurrency -= toRemove;
        _MainUI.UpdateCurrencyAmount(_currentCurrency);
    }
}
