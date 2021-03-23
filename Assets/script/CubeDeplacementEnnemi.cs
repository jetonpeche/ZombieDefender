using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacementEnnemi : MonoBehaviour
{
    private NavMeshAgent agent = null;
    public Transform objectif;
    private Animator anim;

    private Transform tempoObjectif;

    private void Awake()
    {        
        objectif = GameObject.FindGameObjectWithTag("Objectif").transform;
        tempoObjectif = objectif;
    }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        DeplacerToObjectif();
    }

    private void Update()
    {
        anim.SetFloat("vitesse", agent.velocity.magnitude);

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
