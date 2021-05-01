using UnityEngine;

public class ZoneDectection : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque = null;
    [SerializeField] private Arme[] armeActuelle = null;
    [SerializeField] private ArmeViserRepos armeViserRepos = null;

    [SerializeField] private LayerMask layerCibler;
    [SerializeField] private float radius;
    [SerializeField] private bool uniteTourelle;

    [SerializeField] private bool estEnnemi = false;

    [HideInInspector] public bool bloquerPosReposArme, tournerTourelle;

    private CubeDeplacement cubeDeplacement = null;

    private void Start()
    {
        if(uniteTourelle)
            cubeDeplacement = transform.parent.gameObject.GetComponent<CubeDeplacement>();

        BouttonActiverCibleAuto(true);
    }

    public void BouttonActiverCibleAuto(bool _state)
    {
        if (_state)
            InvokeRepeating("Detection", 0f, 0.5f);
        else
            CancelInvoke("Detection");
    }

    // Detection automatique et declanche attaque
    private void Detection()
    {
        Collider[] _tabCol = Physics.OverlapSphere(transform.position, radius, layerCibler);

        // ennemis a porter
        if (_tabCol.Length > 0)
        {
            if(armeViserRepos != null)
                armeViserRepos.Viser();

            // evite de changer de cible quand une nouvelle est a porte
            if (cubeAttaque.GetCible() == null)
                cubeAttaque.Cibler(_tabCol[0].transform);

            // script sur ennemi
            if (estEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().StopDeplacement();

            if (!cubeAttaque.IsInvoking("Attaquer"))
            {
                foreach (Arme arme in armeActuelle)
                {
                    cubeAttaque.InvokeRepeating("Attaquer", 0f, arme.GetCadenceTir());
                }
            }


            tournerTourelle = true;
        }
        else
        {
            if (uniteTourelle && tournerTourelle)
            {
                cubeDeplacement.Invoke("InitialPosTourelle", 2f);
                tournerTourelle = false;
            }

            cubeAttaque.CancelInvoke("Attaquer");
            cubeAttaque.Cibler(null);

            // bloque la pose repos quand c'est une attaque cible
            if (!bloquerPosReposArme && armeViserRepos != null)
                armeViserRepos.Repos();

            // script sur ennemi
            if (estEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().DeplacerToObjectif();                        
        }
    }

    public float GetRadius()
    {
        return radius;
    }

    public Arme[] GetArmes()
    {
        return armeActuelle;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
