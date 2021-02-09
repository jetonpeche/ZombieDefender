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

    private bool selectionMultiple;

    private void Start()
    {
        listeUniteSelectionne = new List<GameObject>();
    }

    private void Update()
    {
        Ray rayon = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;

        // selectionner unitée(s)
        if(Input.GetMouseButtonDown(0) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerCopain))
        {
            if(!selectionMultiple)
            {
                if (listeUniteSelectionne.Count > 0)
                {
                    foreach  (GameObject _obj in listeUniteSelectionne)
                    {
                        _obj.GetComponent<CubeClick>().Clack();
                    }
                }
                    
                listeUniteSelectionne.Clear();
            }

            _hit.transform.GetComponent<CubeClick>().Click();
            listeUniteSelectionne.Add(_hit.transform.gameObject);
        }

        // Attaquer cible
        else if (Input.GetMouseButtonDown(1) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerEnnemi))
        {
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_hit.transform.gameObject, _item.GetComponentInChildren<ZoneDectection>().GetRadius());
            }
        }

        // deplacement
        else if(Input.GetMouseButtonDown(1) && Physics.Raycast(rayon, out _hit, Mathf.Infinity, layerTerrain))
        {
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeDeplacement>().Deplacer(_hit.point);
            }
        }

        // voir la bar de vie des unités selectionné
        else if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeClick>().BouttonMontrerBarVie(true);
            }
        }

        // cacher la bar de vie
        else if(Input.GetKeyUp(KeyCode.LeftControl))
        {
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeClick>().BouttonMontrerBarVie(false);
            }
        }

        // selectionner plusieurs unité
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            selectionMultiple = true;
            Debug.Log("shift");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            selectionMultiple = false;
            Debug.Log("plus shift");
        }
    }
}
