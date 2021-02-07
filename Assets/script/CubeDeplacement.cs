using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacement : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque;
    [SerializeField] private ZoneDectection zoneDectection;

    private NavMeshAgent agent;

    private GameObject cible;
    private float porter;
    private bool deplacerAporterCible;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(deplacerAporterCible)
        {
            // se deplacer jusqu'a porter pour attaquer
            if (Vector3.Distance(transform.position, cible.transform.position) > porter)
            {
                agent.SetDestination(cible.transform.position);
            }    
            // lancer l'attaque sur l'enemi
            else if(Vector3.Distance(transform.position, cible.transform.position) <= porter)
            {
                deplacerAporterCible = false;
                agent.SetDestination(transform.position);

                zoneDectection.BouttonActiverCibleAuto(false);

                cubeAttaque.cible = cible;

                if (!cubeAttaque.IsInvoking("Attaquer"))
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);
            }
        }
    }

    public void Deplacer(Vector3 _hit)
    {
        zoneDectection.BouttonActiverCibleAuto(true);
        agent.SetDestination(_hit);
    }

    public void DeplacerVersEnnemi(GameObject _cible, float _porter)
    {
        cible = _cible;
        porter = _porter;

        deplacerAporterCible = true;
    }
}
