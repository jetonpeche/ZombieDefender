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

    // Detection automatique et attaque automatique
    private void Detection()
    {
        Collider[] _tabCol = Physics.OverlapSphere(transform.position, radius, layer);

        if (_tabCol.Length > 0)
        {
            // evite de changer de cible quand une nouvelle est a porte
            if(cubeAttaque.cible == null)
                cubeAttaque.cible = _tabCol[0].gameObject;

            if (!cubeAttaque.IsInvoking("Attaquer"))
                cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);
        }
        else
            cubeAttaque.CancelInvoke("Attaquer");
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
