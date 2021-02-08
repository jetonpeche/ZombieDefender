using UnityEngine;

public class ZoneDectection : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float radius;

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
        Collider[] _tabCol = Physics.OverlapSphere(transform.position, radius, layer);

        // ennemis a porter
        if (_tabCol.Length > 0)
        {
            // evite de changer de cible quand une nouvelle est a porte
            if(cubeAttaque.cibleVie == null)
                cubeAttaque.cibleVie = _tabCol[0].gameObject.GetComponent<CubeVie>();

            if(!cubeAttaque.IsInvoking("Attaquer"))
                cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);     
        }
        else
        {
            cubeAttaque.CancelInvoke("Attaquer");
            cubeAttaque.cibleVie = null;
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
