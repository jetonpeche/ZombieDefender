using UnityEngine;

public class CubeAttaque : MonoBehaviour
{

    [HideInInspector] public CubeVie cibleVie;
    [SerializeField] private ZoneDectection zoneDectection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform canonArme;
    [SerializeField] private int vitesseProjectile;

    private float porter;

    private void Start()
    {
        porter = zoneDectection.GetRadius();
    }

    private void Update()
    {
        if(cibleVie != null)
            transform.LookAt(cibleVie.gameObject.transform);     
    }

    public void Attaquer()
    {
        // permet de ne pas tirer sur un mur avec un ennemi derriere
        RaycastHit _hit;
        if (Physics.Raycast(transform.position, transform.forward, out _hit, porter))
        {
            if (_hit.transform.gameObject.tag == "ennemi")
            {
                GameObject _obj = Instantiate(projectile, canonArme.position, Quaternion.identity);

                _obj.GetComponent<Projectile>().Initialiser(transform.position, porter, "ennemi");
                _obj.GetComponent<Rigidbody>().velocity = canonArme.forward * vitesseProjectile;
            }
            else
                cibleVie = null;
        }
        else
        {
            cibleVie = null;
            zoneDectection.BouttonActiverCibleAuto(true);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 5);
    }
}
