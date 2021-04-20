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
    [SerializeField] private GameObject marine;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject spnkr;
    [SerializeField] private GameObject scorpion;

    public void InstancierUniteMarine(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(marine, _posDepart, _posArrive);
    }

    public void InstancierUniteSniper(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(sniper, _posDepart, _posArrive);
    }

    public void InstancierUniteSpnkr(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(spnkr, _posDepart, _posArrive);
    }

    public void InstancierUniteScorpion(Vector3 _posDepart, Vector3 _posArrive)
    {
        Instancier(scorpion, _posDepart, _posArrive);
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


