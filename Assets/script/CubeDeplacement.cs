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
    private float porter, cadenceTirArme;
    private bool deplacerAporterCible;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        armeViserRepos = GetComponent<ArmeViserRepos>();
    }

    void Update()
    {
        // Ne peut pas reculer ou droite gauche
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

                cubeAttaque.Cibler(cible.transform);

                cible = null;

                if (!cubeAttaque.IsInvoking("Attaquer"))
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, cadenceTirArme);
            }
        }
    }

    public void Deplacer(Vector3 _hit)
    {
        agent.SetDestination(_hit);

        if(cubeAttaque.GetCible() == null)
            armeViserRepos.Repos();
    }

    public void DeplacerVersEnnemi(GameObject _cible, float _porter)
    {
        cible = _cible;
        porter = _porter;

        cadenceTirArme = cubeAttaque.armeActuelle.GetCadenceTir();
        zoneDectection.bloquerPosReposArme = true;
        armeViserRepos.Viser();

        deplacerAporterCible = true;
    }
}
