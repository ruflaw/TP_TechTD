using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Tower_Spine : MonoBehaviour
{
    private List<GameObject> _enemy = new List<GameObject>();
    [SerializeField] private float shootDelay;
    [SerializeField] private int damage;

    private float nextShoot;
    private GameObject target;

    private void Update()
    {
        if (_enemy.Count > 0 && Time.time >= nextShoot)
        {
            AcquireTarget();
            nextShoot = Time.time + shootDelay;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemy.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDeath(other.gameObject);
        }
    }

    public void EnemyDeath(GameObject enemy)
    {
        _enemy.Remove(enemy);
    }

    private void AcquireTarget()
    {
        for (int i = _enemy.Count - 1; i >= 0; i--)
        {
            if (_enemy[i] == null)
            {
                _enemy.RemoveAt(i);
            }
        }

        foreach (GameObject enemy in _enemy)
        {
            enemy.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
