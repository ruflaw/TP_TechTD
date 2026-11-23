using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;
    [SerializeField]
    private int sous;
    [SerializeField] private GameObject world;

    void Start()
    {
        life = max_life;
    }

    
    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life < 0)
        {
            world.GetComponent<Currency_Manager>().AddCurrency(sous);
            Destroy(this.gameObject);
        }
    }
}
