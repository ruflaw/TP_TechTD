using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;
    [SerializeField]
    private int sous;
    private GameObject world;

    void Start()
    {
        life = max_life;
        
    }

    public void AssignWorld(GameObject world_)
    {
        world = world_;
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
