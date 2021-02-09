﻿using UnityEngine;

public class ZoneDectection : MonoBehaviour
{
    [SerializeField] private CubeAttaque cubeAttaque;
    [SerializeField] private LayerMask layer;
    [SerializeField] private float radius;
    [SerializeField] [Header("remplir sur les ennemis")] private string tagEnnemi;

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
            if (cubeAttaque.cible == null)
                cubeAttaque.cible = _tabCol[0].transform;

            // script sur ennemi
            if (gameObject.transform.parent.transform.tag == tagEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().StopDeplacement();

            if (!cubeAttaque.IsInvoking("Attaquer"))
                cubeAttaque.InvokeRepeating("Attaquer", 0f, 0.5f);
        }
        else
        {
            cubeAttaque.CancelInvoke("Attaquer");
            cubeAttaque.cible = null;

            // script sur ennemi
            if (gameObject.transform.parent.transform.tag == tagEnnemi)
                transform.parent.GetComponent<CubeDeplacementEnnemi>().DeplacerToObjectif();             
        }
    }

    public float GetRadius()
    {
        return radius;
    }

    public string GetTagEnnemi()
    {
        return tagEnnemi;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
