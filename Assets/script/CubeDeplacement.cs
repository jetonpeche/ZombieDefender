using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(ArmeViserRepos))]
public class CubeDeplacement : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque;
    [SerializeField] private ZoneDectection zoneDectection;
    [SerializeField] private Animator anim;

    private ArmeViserRepos armeViserRepos;
    private NavMeshAgent agent;
    private GameObject cible;
    private float porter;
    private bool deplacerAporterCible;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        armeViserRepos = GetComponent<ArmeViserRepos>();
    }

    void Update()
    {
        // POURQUOI TU NE MARCHES PAS !!!
        //anim.SetFloat("vitesse", agent.velocity.magnitude);

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

                cubeAttaque.cible = cible.transform;

                cible = null;

                if (!cubeAttaque.IsInvoking("Attaquer"))
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);
            }
        }
    }

    public void Deplacer(Vector3 _hit)
    {
        agent.SetDestination(_hit);

        if(cubeAttaque.cible == null)
            armeViserRepos.Repos();
    }

    public void DeplacerVersEnnemi(GameObject _cible, float _porter)
    {
        cible = _cible;
        porter = _porter;

        zoneDectection.bloquerPosReposArme = true;
        armeViserRepos.Viser();

        deplacerAporterCible = true;       
    }
}
