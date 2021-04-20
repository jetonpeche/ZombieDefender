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
            int _index = 0;
            List<Vector3> _listOffsetDeplacement = CalculOffsetPositon();
            
            foreach (GameObject _item in listeUniteSelectionne)
            {
                _item.GetComponent<CubeDeplacement>().Deplacer(_hit.point + _listOffsetDeplacement[_index]);
                _index = (_index + 1) % _listOffsetDeplacement.Count;
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
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            selectionMultiple = false;
        }
    }

    public void UniteMorte(GameObject _obj)
    {
        listeUniteSelectionne.Remove(_obj);
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
}
