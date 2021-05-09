using UnityEngine;

public class ZoneDectection : MonoBehaviour
{
    #region variables

    [SerializeField] private CubeAttaque cubeAttaque = null;
    [SerializeField] private Arme[] armeActuelle = null;
    [SerializeField] private ArmeViserRepos armeViserRepos = null;
    
    [SerializeField] private float radius;
    [SerializeField] private bool uniteTourelle;

    [SerializeField] private bool estEnnemi = false;

    [HideInInspector] public bool bloquerPosReposArme, tournerTourelle;

    private CubeDeplacement cubeDeplacement = null;
    private LayerMask layerCibler;

    #endregion

    private void Start()
    {
        layerCibler = cubeAttaque.GetLayerCible();

        if(uniteTourelle)
            cubeDeplacement = transform.parent.gameObject.GetComponent<CubeDeplacement>();

        BouttonActiverCibleAuto(true);
    }

    #region fonctions publics

    public void BouttonActiverCibleAuto(bool _state)
    {
        if (_state)
            InvokeRepeating("Detection", 0f, 0.5f);
        else
            CancelInvoke("Detection");
    }

    public float GetRadius()
    {
        return radius;
    }

    public Arme[] GetArmes()
    {
        return armeActuelle;
    }

    public LayerMask GetLayer()
    {
        return layerCibler;
    }

    public void StopAttaque()
    {
        cubeAttaque.CancelInvoke("Attaquer");
    }

    #endregion

    // Detection automatique et declanche attaque
    private void Detection()
    {
        Collider[] _tabCol = Physics.OverlapSphere(transform.position, radius, layerCibler);

        // ennemis a porter
        if (_tabCol.Length > 0)
        {
            if (armeViserRepos != null)
                armeViserRepos.Viser();

            // evite de changer de cible quand une nouvelle est a porte
            if (cubeAttaque.GetCible() == null)
                cubeAttaque.Cibler(_tabCol[0].transform);

            // script sur ennemi
            if (estEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().StopDeplacement();

            if (!cubeAttaque.IsInvoking("Attaquer"))
            {
                cubeAttaque.InvokeRepeating("Attaquer", 0f, armeActuelle[0].GetCadenceTir());
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

            StopAttaque();
            cubeAttaque.Cibler(null);

            // bloque la pose repos quand c'est une attaque cible
            if (!bloquerPosReposArme && armeViserRepos != null)
                armeViserRepos.Repos();

            // script sur ennemi
            if (estEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().DeplacerToObjectif();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
