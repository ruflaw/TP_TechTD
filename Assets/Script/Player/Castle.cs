using System.Collections;
using UnityEngine;

public class Castle : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;


    void Awake()
    {
        life = max_life;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            int damage = other.GetComponent<Enemy>().damage;
            life -= damage;
            if (life <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
