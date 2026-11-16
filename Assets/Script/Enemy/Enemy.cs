using System.Collections;
using UnityEngine;
using UnityEngine.Splines;
using System.Collections.Generic;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public SplineContainer splineContainer;
    [SerializeField]
    private SplineAnimate splineAnimate;
    private float remainingPath;
    private float totalPath;
    public int damage;
    public float attackDelay;
    [SerializeField]
    private int maxHealth;
    private int health = 1;
    private List<Tower> _tower = new List<Tower>();

    private Collider currentWall;

    private void Awake()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && currentWall == null)
        {
            currentWall = other;
            splineAnimate.Pause();
            StartCoroutine(WaitForWallDestruction());
        }

        else if (other.CompareTag("Castle"))
        {
            StartCoroutine(DestructionOnWall());
        }
    }

    private IEnumerator WaitForWallDestruction()
    {
        while (currentWall != null && currentWall.gameObject.activeInHierarchy)
        {
            yield return new WaitForSeconds(attackDelay);
        }

        currentWall = null;
        splineAnimate.Play();
    }

    private IEnumerator DestructionOnWall()
    {
        yield return new WaitForSeconds(1);
        Death();
    }

    public void AddTower(Tower tower)
    {
        _tower.Add(tower);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            foreach (Tower tower in _tower)
            {
                tower.EnemyDeath(this.gameObject);
            }
            Death();
        }
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
