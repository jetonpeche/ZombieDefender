using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacementEnnemi : MonoBehaviour
{
    [SerializeField] private string tagObjectif;

    private NavMeshAgent agent = null;
    private Transform objectif;
    private Animator anim;

    private void Awake()
    {
        objectif = GameObject.FindWithTag(tagObjectif).transform;
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
        agent.isStopped = true;
    }

    public void DeplacerToObjectif()
    {
        agent.isStopped = false;
    }
}
