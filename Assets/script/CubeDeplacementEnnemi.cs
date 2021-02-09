using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacementEnnemi : MonoBehaviour
{
    private NavMeshAgent agent = null;
    private Transform objectif;

    private Transform tempoObjectif;

    private void Awake()
    {
        objectif = GameObject.FindGameObjectWithTag("Objectif").transform;
        tempoObjectif = objectif;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        DeplacerToObjectif();
    }

    private void Update()
    {
        if(!Objectif.objectifDetruit)
            agent.SetDestination(objectif.position);
    }

    public void StopDeplacement()
    {
        objectif = transform;
    }

    public void DeplacerToObjectif()
    {
        objectif = tempoObjectif;
    }
}
