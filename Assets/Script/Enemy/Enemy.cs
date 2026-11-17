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
    public int damage;
    public float attackDelay;

    private Collider currentWall;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Wall") && currentWall == null)
        {
            currentWall = other;
            splineAnimate.Pause();
            StartCoroutine(WaitForWallDestruction());
        }
    }

    private IEnumerator WaitForWallDestruction()
    {
        while (currentWall != null && currentWall.gameObject.activeInHierarchy)
        {
            currentWall.GetComponent<Health>().TakeDamage(damage);
            Debug.Log(currentWall.gameObject);
            yield return new WaitForSeconds(attackDelay);
        }

        currentWall = null;
        splineAnimate.Play();
    }
}
