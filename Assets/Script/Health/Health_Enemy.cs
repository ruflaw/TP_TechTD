using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;
    [SerializeField]
    private int sous;
    [SerializeField] private Image _healthBarFill;
    private float _healthBarFillingPercent = 1;
    private GameObject world;

    void Start()
    {
        life = max_life;
        _healthBarFill.fillAmount = _healthBarFillingPercent;
    }

    public void AssignWorld(GameObject world_)
    {
        world = world_;
    }

    
    public void TakeDamage(int damage)
    {
        life -= damage;
        float tempLife = life;
        float tempMaxLife = max_life;
        _healthBarFillingPercent = tempLife / tempMaxLife;
        if (life < 0)
        {
            world.GetComponent<Currency_Manager>().AddCurrency(sous);
            Destroy(this.gameObject);
        }
        _healthBarFill.fillAmount = _healthBarFillingPercent;
    }
}
