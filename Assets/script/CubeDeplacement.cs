using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class CubeDeplacement : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque = null;
    [SerializeField] private ZoneDectection zoneDectection = null;
    [SerializeField] private Animator anim = null;
    [SerializeField] private Transform tourelleChar = null;

    private ArmeViserRepos armeViserRepos = null;
    private NavMeshAgent agent;
    private GameObject cible = null;
    private float porter;
    private Arme[] armes;
    private bool deplacerAporterCible, tournerTourelle;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        armeViserRepos = GetComponent<ArmeViserRepos>();
    }

    void Update()
    {
        // Ne peut pas reculer ou droite gauche
        //anim.SetFloat("vitesse", agent.velocity.magnitude);

        DeplacerAporterCible();

        if(tournerTourelle)
            TournerTourelle();
    }

    public void Deplacer(Vector3 _hit)
    {
        agent.SetDestination(_hit);

        if(cubeAttaque.GetCible() == null && armeViserRepos != null)
            armeViserRepos.Repos();
    }

    public void DeplacerVersEnnemi(GameObject _cible, float _porter)
    {
        cible = _cible;
        porter = _porter;

        armes = zoneDectection.GetArmes();
        zoneDectection.bloquerPosReposArme = true;
        tournerTourelle = false;

        if(armeViserRepos != null)
            armeViserRepos.Viser();

        deplacerAporterCible = true;
    }

    public void InitialPosTourelle()
    {
        tournerTourelle = true;
    }

    private void DeplacerAporterCible()
    {
        if (deplacerAporterCible)
        {
            // se deplacer jusqu'a porter pour attaquer
            if (Vector3.Distance(transform.position, cible.transform.position) > porter)
            {
                agent.SetDestination(cible.transform.position);
            }
            // lancer l'attaque sur l'ennemi
            else if (Vector3.Distance(transform.position, cible.transform.position) <= porter)
            {
                deplacerAporterCible = false;
                zoneDectection.BouttonActiverCibleAuto(false);

                agent.SetDestination(transform.position);

                cubeAttaque.Cibler(cible.transform);

                cible = null;

                if (!cubeAttaque.IsInvoking("Attaquer"))
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, armes[0].GetCadenceTir());
            }
        }
    }

    private void TournerTourelle()
    {
        tourelleChar.rotation = Quaternion.Lerp(tourelleChar.rotation, transform.rotation, 2f * Time.deltaTime);

        if (tourelleChar.rotation == transform.rotation)
            tournerTourelle = false;
    }
}
