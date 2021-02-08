using UnityEngine;

public class CubeAttaque : MonoBehaviour
{

    [HideInInspector] public CubeVie cibleVie;
    [SerializeField] private ZoneDectection zoneDectection;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform canonArme;
    [SerializeField] private int vitesseProjectile;

    private float porter;
    private LayerMask layer;

    private void Start()
    {
        layer = Click.instance.GetLayerEnnemi();
        porter = zoneDectection.GetRadius();
    }

    private void Update()
    {
        if(cibleVie != null)
            transform.LookAt(cibleVie.gameObject.transform);     
    }

    public void Attaquer()
    {
        if (Physics.Raycast(transform.position, transform.forward, porter, layer) && cibleVie.EstEnVie())
        {
            GameObject _obj = Instantiate(projectile, canonArme.position, Quaternion.identity);

            _obj.GetComponent<Projectile>().Initialiser(transform.position, porter, "ennemi");
            _obj.GetComponent<Rigidbody>().velocity = canonArme.forward * vitesseProjectile;
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
