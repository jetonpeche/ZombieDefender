using UnityEngine;

public class CreerUnite : MonoBehaviour
{
    #region Singletoon
    public static CreerUnite instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [Header("Marines")]
    public GameObject unitePistolet, uniteSniper;

    public void InstancierUnitePistolet(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(unitePistolet, _posDepart, _posArrive);
    }

    public void InstancierUniteSniper(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(uniteSniper, _posDepart, _posArrive);
    }

    private void Instancier(GameObject _unite, Vector3 _posDepart, Vector3 _posArrive)
    {
        if (Inventaire.instance.NombreUniteMaxAtteint())
        {
            GameObject _obj = Instantiate(_unite, _posDepart, Quaternion.identity);
            _obj.GetComponent<CubeDeplacement>().Deplacer(_posArrive);

            Inventaire.instance.AjouterUniteJoueur();
        }
    }
}


