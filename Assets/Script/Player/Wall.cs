using System.Collections;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField]
    private int max_life;
    private int life = 1;


    void Awake()
    {
        life = max_life; // Initialise la vie
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
           StartCoroutine(Fight(other));
        }
    }

    private IEnumerator Fight(Collider other)
    {
        while (life > 0 && other.gameObject.activeInHierarchy)
        {
            int damage = other.GetComponent<Enemy>().damage;
            life -= damage;
            yield return new WaitForSeconds(other.GetComponent<Enemy>().attackDelay);
        }
        Destroy(gameObject);
    }
}
