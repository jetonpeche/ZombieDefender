using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacementEnnemi : MonoBehaviour
{
    [SerializeField] private string tagObjectif = "copain,objectif";

    private NavMeshAgent agent = null;
    private Transform objectif;
    private Animator anim;

    private void Awake()
    {
        objectif = GameObject.FindWithTag(tagObjectif).transform;
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        DeplacerToObjectif();
    }

    private void Update()
    {
        if(anim != null)
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
