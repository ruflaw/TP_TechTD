using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

[Serializable]
public struct Path_
{
    public GameObject spawn; //Point de spawn de la wave (a bouger)
    public SplineContainer path; //SPline que va suivre le groupe
    [SerializeField]
    public List<Enemy_> _enemies; //Liste des Enemy_ dans la vague
}

[Serializable]
public struct Enemy_ //Pour choisir le type d'ennemis et sa quantité dans un groupe 
{
    public Wave_Generator.Enemy_Type enemy; //Type d'ennemi cf enum Enemy_Type
    public int number; //Quantité d'ennemi
    public float delay; //delais avant le prochain menbre du groupe
    public float delay_end; //delais avant le prochain groupe
}

public class Wave_Generator : MonoBehaviour
{
    [Header("Liste d'ennemis")]
    [SerializeField]
    public List<GameObject> enemy_type; //Liste des prefab d'ennemis, associer au type de l'enum

    [SerializeField]
    public List<Path_> _path;

    public enum Enemy_Type //Type d'ennemis, associé a la liste enemy-type
    {
        Soldier, Exoskeleton, Prisonner,
    }

    void Awake()
    {
        StartCoroutine(Path_Repartition());
    }

    private IEnumerator Path_Repartition()
    {
        foreach (Path_ path in _path) // Lance une couroutine de spawn par ligne
        {
            StartCoroutine(Wave_Spawner(path.spawn, path.path, path._enemies));
        }
        yield return null;
    }

    private IEnumerator Wave_Spawner(GameObject spawn, SplineContainer path, List<Enemy_> _enemy)
    {
        foreach (Enemy_ enemy in _enemy) // Pour chaque struct enemy on les fait spawn
        {
            int count = enemy.number;
            while(count > 0) // tant qu'on a pas fait spawn la quantiité d'enemi dans le struct
            {
                int position = (int)enemy.enemy; // A changé
                GameObject enemy_spawn = enemy_type[position]; // A changé
                GameObject a = Instantiate(enemy_spawn, spawn.transform.position, spawn.transform.rotation); // Spawn d'un mob
                a.GetComponent<Enemy>().splineContainer = path; // Renseigne a l'enemy son spline
                a.GetComponent<SplineAnimate>().Container = path; // Renseigne a l'enemy son spline
                a.GetComponent<SplineAnimate>().Play(); // Lance le mouvement
                a.GetComponent<Health>().AssignWorld(this.gameObject);
                count--;
                yield return new WaitForSeconds(enemy.delay); // Delay au seins d'un groupe d'enemy
            }
            yield return new WaitForSeconds(enemy.delay_end); // delay entre les groupe d'enemy
        }
        yield return null;
    }
}
