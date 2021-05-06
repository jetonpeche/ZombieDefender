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

    #region variables

    [SerializeField] private LayerMask layerTerrain;
    [SerializeField] private LayerMask layerEnnemi;
    [SerializeField] private LayerMask layerCopain;

    [SerializeField] private RectTransform zoneDeSelection;

    [SerializeField] private List<GameObject> listeUnite = new List<GameObject>();

    public List<GameObject> listeUniteSelectionne = null;
    private bool selectionMultipleMainActif, selectionMultiple;
    private Vector2 posSourisDepart;

    #endregion


    private void Start()
    {
        listeUniteSelectionne = new List<GameObject>();

        zoneDeSelection.gameObject.SetActive(false);
    }

    private void Update()
    {
        Ray _rayon = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit _hit;

        #region Selection unité(s)

        // selection multiple avec zone de selection (prit sur youtube)
        // https://www.youtube.com/watch?v=cd7pgnw5OLA&ab_channel=Zenva

        // selectionner unitée(s)
        if (Input.GetMouseButtonDown(0) && Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerCopain))
        {
            if (!selectionMultipleMainActif)
                DeSelectionnerUnite();

            SelectionUnite(_hit.transform);
        }

        // zone de selection
        else if(Input.GetMouseButtonDown(0) && Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerTerrain))
        {
            DeSelectionnerUnite();

            zoneDeSelection.gameObject.SetActive(true);
            selectionMultiple = true;
            posSourisDepart = Input.mousePosition;
        }

        if (Input.GetMouseButton(0) && selectionMultiple)
        {
            UpdateZoneSelection(Input.mousePosition);
        }

        if (Input.GetMouseButtonUp(0) && selectionMultiple)
        {
            selectionMultiple = false;
            zoneDeSelection.gameObject.SetActive(false);

            // position bas gauche de la boite
            Vector2 _min = zoneDeSelection.anchoredPosition - (zoneDeSelection.sizeDelta / 2);

            // position haut droite de la boite
            Vector2 max = zoneDeSelection.anchoredPosition + (zoneDeSelection.sizeDelta / 2);

            // voir si position l'unité est dans la zone de selection
            foreach (GameObject _unite in listeUnite)
            {
                // convertie la position de l'unité en position de l'ecran
                Vector3 _posEcran = Camera.main.WorldToScreenPoint(_unite.transform.position);

                // verif que l'unite est dans la zone
                if (_posEcran.x > _min.x && _posEcran.x < max.x && _posEcran.y < max.y && _posEcran.y > _min.y)
                {
                    SelectionUnite(_unite.transform);
                }
            }
        }

        // selectionner toutes les unités (&)
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            DeSelectionnerUnite();

            foreach (var item in listeUnite)
            {
                listeUniteSelectionne.Add(item);
            }
        }

        #endregion

        #region Actions

        // Attaquer cible
        if (Input.GetMouseButtonDown(1) && Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerEnnemi))
        {
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeDeplacement>().DeplacerVersEnnemi(_hit.transform.gameObject, _item.GetComponentInChildren<ZoneDectection>().GetRadius());
            }
        }

        // deplacement
        else if (Input.GetMouseButtonDown(1) && Physics.Raycast(_rayon, out _hit, Mathf.Infinity, layerTerrain))
        {
            int _index = 0;
            List<Vector3> _listOffsetDeplacement = CalculOffsetPositon();

            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeDeplacement>().Deplacer(_hit.point + _listOffsetDeplacement[_index]);
                _index = (_index + 1) % _listOffsetDeplacement.Count;
            }
        }

        #endregion

        #region bar de vie

        // voir la bar de vie des unités selectionné
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            BouttonMontrerBarVie(true);
        }

        // cacher la bar de vie
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            BouttonMontrerBarVie(false);
        }

        #endregion

        #region selection multiple à la main

        // selectionner plusieurs unité a la main
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            selectionMultiple = true;
        }

        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            selectionMultiple = false;
        }

        #endregion
    }

    #region fonctions publics

    public void UniteMorte(GameObject _obj)
    {
        listeUniteSelectionne.Remove(_obj);
        listeUnite.Remove(_obj);
    }

    public void UniteCreer(GameObject _obj)
    {
        listeUnite.Add(_obj);
    }

    #endregion

    #region fonctions privates

    private void SelectionUnite(Transform _hit)
    {
        CubeClick _cubeClick = _hit.GetComponent<CubeClick>();
        _cubeClick.estSelectionne = true;
        _cubeClick.Click();

        listeUniteSelectionne.Add(_hit.gameObject);
    }

    private void DeSelectionnerUnite()
    {
        CubeClick _cubeClick;

        foreach (GameObject _obj in listeUniteSelectionne)
        {
            _cubeClick = _obj.GetComponent<CubeClick>();
            _cubeClick.estSelectionne = false;
            _cubeClick.Clack();
        }

        listeUniteSelectionne.Clear();
    }

    private void UpdateZoneSelection(Vector2 _posActuelleSouris)
    {
        if (!zoneDeSelection.gameObject.activeInHierarchy)
            zoneDeSelection.gameObject.SetActive(true);

        float _largeur = _posActuelleSouris.x - posSourisDepart.x;
        float _hauteur = _posActuelleSouris.y - posSourisDepart.y;

        // modifier la taille de la zone
        zoneDeSelection.sizeDelta = new Vector2(Mathf.Abs(_largeur), Mathf.Abs(_hauteur));

        // changer le point de pivot
        zoneDeSelection.anchoredPosition = posSourisDepart + new Vector2(_largeur / 2, _hauteur / 2);
    }

    private void BouttonMontrerBarVie(bool _etat)
    {
        foreach (GameObject _item in listeUniteSelectionne)
        {
            _item.GetComponent<CubeClick>().BouttonMontrerBarVie(_etat);
        }
    }

    private List<Vector3> CalculOffsetPositon()
    {
        List<Vector3> _list = new List<Vector3>();

        // evite de bouclé inutilement
        if (listeUniteSelectionne.Count > 1)
        {
            // char x, z = 8
            // marine x, z = 2

            float _posX = 0;
            float _posZ = 0;

            int _nbUniteSurLigne = 0;
            bool _vehiculeDansSelection = false;

            foreach (var item in listeUniteSelectionne)
            {
                _list.Add(new Vector3(_posX, 0, _posZ));

                if (Tag.PossedeTag("scorpion", item))
                {
                    _vehiculeDansSelection = true;
                    _posX += 8;
                    _nbUniteSurLigne++;
                }
                else
                {
                    _posX += 2;
                    _nbUniteSurLigne++;
                }


                if (_nbUniteSurLigne == 2)
                {
                    _nbUniteSurLigne = 0;

                    if (_vehiculeDansSelection)
                        _posZ += 8;
                    else
                        _posZ += 2;

                    _posX = 0;
                }
            }

            return _list;
        }
        else
        {
            _list.Add(new Vector3(0, 0, 0));
            return _list;
        }
    }

    #endregion
}
