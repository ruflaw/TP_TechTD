using UnityEngine;

public class Health_Wall : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    [SerializeField] private UI_Game_Manager _MainUI;
    [SerializeField] private GameObject _GameOverUI;
    private int life = 1;

    void Start()
    {
        life = max_life;
        _MainUI.UpdateHealthPercent(life/max_life);
    }

    
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life <= 0)
        {
            print("less than 0 hp");
            OnDeath();
        }
        _MainUI.UpdateHealthPercent(life / max_life);
    }

    public void OnDeath()
    {
        print("JustDieDude");
        Instantiate(_GameOverUI);
        Time.timeScale = 0;
    }
}
