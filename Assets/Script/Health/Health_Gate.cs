using System;
using UnityEngine;

public class Health_Wall : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    [SerializeField] private UI_Game_Manager _MainUI;
    [SerializeField] private GameObject _GameOverUI;
    private int life = 1;
    private float lifePercent = 1f;

    void Start()
    {
        life = max_life;
        _MainUI.UpdateHealthPercent(lifePercent);
    }

    
    public void TakeDamage(int damage)
    {
        if (life - damage <= 0)
        {
            OnDeath();
        }
        life -= damage;
        float templife = life;
        float tempMaxLife = max_life;
        lifePercent = templife/tempMaxLife;
        _MainUI.UpdateHealthPercent(lifePercent);
    }

    public void OnDeath()
    {
        Instantiate(_GameOverUI);
        Time.timeScale = 0;
    }
}
