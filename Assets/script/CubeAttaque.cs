using UnityEngine;

public class CubeAttaque : MonoBehaviour
{
    public Transform cible;  

    [SerializeField] private LayerMask layerEnnemi;
    [SerializeField] private ZoneDectection zoneDectection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform canonArme;
    [SerializeField] private int vitesseProjectile;
    [SerializeField] private string tagCibleProjectile;
    [SerializeField] private int degats;

    private float porter;
    private Transform persoPos;

    private void Start()
    {
        porter = zoneDectection.GetRadius();
        persoPos = transform.parent.gameObject.transform;
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
            GameObject _obj = Instantiate(projectile, canonArme.position, Quaternion.identity);

            _obj.GetComponent<Projectile>().Initialiser(transform.position, porter, tagCibleProjectile, degats, gameObject);
            _obj.GetComponent<Rigidbody>().velocity = canonArme.forward * vitesseProjectile;
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
            GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_cible, porter);
            GetComponent<ArmeViserRepos>().Viser();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * porter);
    }
}
