using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class Tower : MonoBehaviour
{
    private List<GameObject> _enemy = new List<GameObject>();
    [SerializeField] private float shootDelay;
    [SerializeField] private int damage;

    private float nextShoot;
    private GameObject target;

    private void Update()
    {
        AcquireTarget();

        if (target != null && Time.time >= nextShoot)
        {
            Shoot(target);
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
        if (target == enemy) target = null;
    }

    private void Shoot(GameObject target)
    {
        target.GetComponent<Health>().TakeDamage(damage);
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

        target = null;
        float bestRemaining = float.MaxValue;

        foreach (GameObject enemy in _enemy)
        {
            Enemy e = enemy.GetComponent<Enemy>();
            SplineAnimate anim = enemy.GetComponent<SplineAnimate>();

            float remaining = (1f - anim.NormalizedTime) * e.splineContainer.CalculateLength();
            if (remaining < bestRemaining)
            {
                bestRemaining = remaining;
                target = enemy;
            }
        }
    }
}
