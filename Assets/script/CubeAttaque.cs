using UnityEngine;

public class CubeAttaque : MonoBehaviour
{

    public Transform cible;
    [SerializeField] private ZoneDectection zoneDectection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform canonArme;
    [SerializeField] private int vitesseProjectile;
    [SerializeField] private string tagCible;

    private float porter;

    private void Start()
    {
        porter = zoneDectection.GetRadius();
    }

    private void Update()
    {
        if(cible != null)
            transform.LookAt(cible);     
    }

    public void Attaquer()
    {
        // permet de ne pas tirer sur un mur avec un ennemi derriere
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, porter))
        {
            if (_hit.transform.gameObject.tag == tagCible)
            {
                GameObject _obj = Instantiate(projectile, canonArme.position, Quaternion.identity);

                _obj.GetComponent<Projectile>().Initialiser(transform.position, porter, tagCible, GetComponent<CubeDeplacementEnnemi>() ? gameObject : null);
                _obj.GetComponent<Rigidbody>().velocity = canonArme.forward * vitesseProjectile;
            }
            else
                cible = null;
        }
        else
        {
            cible = null;
            zoneDectection.BouttonActiverCibleAuto(true);
        }
    }

    public void Repliquer(GameObject _cible)
    {
        if(cible == null && GetComponent<CubeDeplacement>())
        {
            GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_cible, porter);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * porter);
    }
}
