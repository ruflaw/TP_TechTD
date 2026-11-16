using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[SelectionBase]
public class Tower : MonoBehaviour
{
    private List<GameObject> _enemy = new List<GameObject>();
    [SerializeField]
    private float shootDelay;
    private float nextShoot;
    private GameObject target;
    [SerializeField]
    private int damage;

    private void Update()
    {
        if (_enemy.Count > 0)
        {
            StartCoroutine(AcquireTarget());
            if (target != null && Time.time >= nextShoot)
            {
                Shoot(target);
                nextShoot = Time.time + shootDelay;
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            _enemy.Add(other.gameObject);
            other.GetComponent<Enemy>().AddTower(this.gameObject.GetComponent<Tower>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyDeath(other.gameObject);
        }
    }

    public void EnemyDeath(GameObject target)
    {
        _enemy.Remove(target);
    }

    private void Shoot(GameObject target)
    {
        target.GetComponent<Enemy>().TakeDamage(damage);
        
    }

    private IEnumerator AcquireTarget()
    {
        float remainingPath = 0;
        float targetRemaningPath = 0;
        foreach (GameObject enemy in _enemy)
        {
            remainingPath = (1f - enemy.GetComponent<SplineAnimate>().NormalizedTime) * enemy.GetComponent<Enemy>().splineContainer.CalculateLength();
            if (remainingPath < targetRemaningPath)
            {
                targetRemaningPath = remainingPath;
                target = enemy;
            }

            else if(targetRemaningPath == 0)
            {
                targetRemaningPath = remainingPath;
                target = enemy;
            }
        }

        yield return new WaitForSeconds(shootDelay);
    }
}
