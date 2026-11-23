using UnityEngine;

public class Health_Root : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;

    void Start()
    {
        life = max_life;

    }

    public void TakeDamage(int damage)
    {
        life -= damage;
        if (life < 0)
        {
            Destroy(this.gameObject);
        }
    }
}
