using UnityEngine;

public class CubeAttaque : MonoBehaviour
{
    [SerializeField] private LayerMask layerEnnemi;
    [SerializeField] private ZoneDectection zoneDectection = null;
    [SerializeField] private Transform persoPos;
    [SerializeField] private Arme[] armeActuelle;

    public Transform cible;
    private float porter;

    private void Start()
    {
        porter = zoneDectection.GetRadius();
        armeActuelle = zoneDectection.GetArmes();
    }

    private void Update()
    {
        if(cible != null)
            persoPos.LookAt(cible);
    }

    public void Attaquer()
    {
        // permet de ne pas tirer sur un mur avec un ennemi derriere et ignore les collisions entre raycast
        if (Physics.Raycast(transform.position, transform.forward, porter, layerEnnemi, QueryTriggerInteraction.Ignore))
        {
            foreach (Arme arme in armeActuelle)
            {
                arme.Tirer();
            }
        }
        else
        {
            cible = null;
            zoneDectection.bloquerPosReposArme = false;
            zoneDectection.BouttonActiverCibleAuto(true);
        }
    }

    public void Repliquer(GameObject _cible)
    {
        if(cible == null)
        {
            armeActuelle[0].GetPerso().GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_cible, porter);

            if (GetComponent<ArmeViserRepos>())
                GetComponent<ArmeViserRepos>().Viser();
        }
    }

    public void Cibler(Transform _cible)
    {
        cible = _cible;
    }

    public Transform GetCible()
    {
        return cible;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * porter);
    }
}
