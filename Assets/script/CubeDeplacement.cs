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

    void Update()
    {
        if(deplacerAporterCible)
        {
            // se deplacer jusqu'a porter pour attaquer
            if (Vector3.Distance(transform.position, cible.transform.position) > porter)
            {
                agent.SetDestination(cible.transform.position);
            }    
            // lancer l'attaque sur l'ennemi
            else if(Vector3.Distance(transform.position, cible.transform.position) <= porter)
            {
                deplacerAporterCible = false;
                zoneDectection.BouttonActiverCibleAuto(false);

                agent.SetDestination(transform.position);

                cubeAttaque.cibleVie = cible.GetComponent<CubeVie>();

                cible = null;

                if (!cubeAttaque.IsInvoking("Attaquer"))
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);
            }
        }
    }

    public void Deplacer(Vector3 _hit)
    {
        agent.SetDestination(_hit);
    }

    public void DeplacerVersEnnemi(GameObject _cible, float _porter)
    {
        cible = _cible;
        porter = _porter;

        deplacerAporterCible = true;
    }
}
