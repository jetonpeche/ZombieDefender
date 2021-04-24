﻿using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] listePrefab;
    [SerializeField] private float range = 10f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // spawn sur le navMesh quand il a trouve un espace sur celui ci   
            if(!Inventaire.instance.NombreUniteMaxEnnemiAtteint())
            {
                Vector3 _point;
                if (RandomPoint(transform.position, range, out _point))
                {
                    Instantiate(listePrefab[Random.Range(0, listePrefab.Length)], _point, Quaternion.identity);
                    Inventaire.instance.AjouterUniteEnnemi();
                }
            }
        }
    }

    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        // cherche une place dispo sur le navMesh
        for (int i = 0; i < 30; i++)
        {
            Vector3 _randomPoint = center + Random.insideUnitSphere * range;

            NavMeshHit _hit;
            if (NavMesh.SamplePosition(_randomPoint, out _hit, 1.0f, NavMesh.AllAreas))
            {
                result = _hit.position;
                return true;
            }
        }

        result = Vector3.zero;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}