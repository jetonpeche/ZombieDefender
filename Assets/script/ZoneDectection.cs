using UnityEngine;

public class ZoneDectection : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque = null;
    [SerializeField] private Arme armeActuelle = null;
    [SerializeField] private ArmeViserRepos armeViserRepos = null;

    [SerializeField] private LayerMask layerCibler;
    [SerializeField] private float radius;
    [SerializeField] [Header("remplir sur les ennemis")] private string tagEnnemi;

    [HideInInspector] public bool bloquerPosReposArme;

    private void Start()
    {
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
            armeViserRepos.Viser();

            // evite de changer de cible quand une nouvelle est a porte
            if (cubeAttaque.GetCible() == null)
                cubeAttaque.Cibler(_tabCol[0].transform);

            // script sur ennemi
            if (gameObject.transform.parent.transform.tag == tagEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().StopDeplacement();

            if (!cubeAttaque.IsInvoking("Attaquer"))
                cubeAttaque.InvokeRepeating("Attaquer", 0f, armeActuelle.GetCadenceTir());
        }
        else
        {
            cubeAttaque.CancelInvoke("Attaquer");
            cubeAttaque.Cibler(null);

            // bloque la pose repos quand c'est une attaque cible
            if (!bloquerPosReposArme)
                armeViserRepos.Repos();

            // script sur ennemi
            if (gameObject.transform.parent.transform.tag == tagEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().DeplacerToObjectif();             
        }
    }

    public float GetRadius()
    {
        return radius;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
