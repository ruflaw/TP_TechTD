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

        else if (other.CompareTag("Player") && currentWall == null)
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
            if (currentWall.gameObject.CompareTag("Wall"))
            {
                currentWall.GetComponent<Health_Root>().TakeDamage(damage);
            }
            else if (currentWall.gameObject.CompareTag("Player"))
            {
                currentWall.GetComponent<Health_Wall>().TakeDamage(damage);
            }
                Debug.Log(currentWall.gameObject);
            yield return new WaitForSeconds(attackDelay);
        }

        currentWall = null;
        splineAnimate.Play();
    }
}
