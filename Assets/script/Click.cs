using System.Collections.Generic;
using UnityEngine;

public class Click : MonoBehaviour
{
    #region Singleton

    public static Click instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private LayerMask layerTerrain;
    [SerializeField] private LayerMask layerEnnemi;
    [SerializeField] private LayerMask layerCopain;

    [SerializeField] private List<GameObject> listeUniteSelectionne = null;
    [SerializeField] private GameObject uniteSelectionne = null;

    private delegate void Deplacer(Vector3 _deplacement);
    private Deplacer deplacer;

    private void Start()
    {
        listeUniteSelectionne = new List<GameObject>();
    }

    private void Update()
    {
        Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;

        // selectionner unite selectionne
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerCopain))
        {
            if(listeUniteSelectionne.Count > 0)
                listeUniteSelectionne[0].GetComponent<CubeClick>().Clack();

            listeUniteSelectionne.Clear();
            deplacer = null;

            _hit.transform.GetComponent<CubeClick>().Click();

            listeUniteSelectionne.Add(_hit.transform.gameObject);
            deplacer += _hit.transform.GetComponent<CubeDeplacement>().Deplacer;
        }

        // Attaquer cible
        else if (Input.GetMouseButtonDown(1) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerEnnemi))
        {
            foreach (GameObject element in listeUniteSelectionne)
            {
                element.GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_hit.transform.gameObject, element.GetComponentInChildren<ZoneDectection>().GetRadius());
            }
        }

        // deplacement
        else if(Input.GetMouseButtonDown(1) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerTerrain))
        {
            deplacer(_hit.point);
        }
    }

    public LayerMask GetLayerEnnemi()
    {
        return layerEnnemi;
    }
}
