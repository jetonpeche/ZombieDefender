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

    [Header("Unitées")]
    [SerializeField] private GameObject marine;
    [SerializeField] private GameObject sniper;
    [SerializeField] private GameObject spnkr;
    [SerializeField] private GameObject scorpion;

    public void InstancierUniteMarine(Vector3 _posDepart, Vector3 _posArrive, int _prix)
    {
        Instancier(marine, _posDepart, _posArrive, _prix);
    }

    public void InstancierUniteSniper(Vector3 _posDepart, Vector3 _posArrive, int _prix)
    {
        Instancier(sniper, _posDepart, _posArrive, _prix);
    }

    public void InstancierUniteSpnkr(Vector3 _posDepart, Vector3 _posArrive, int _prix)
    {
        Instancier(spnkr, _posDepart, _posArrive, _prix);
    }

    public void InstancierUniteScorpion(Vector3 _posDepart, Vector3 _posArrive, int _prix)
    {
        Instancier(scorpion, _posDepart, _posArrive, _prix);
    }

    private void Instancier(GameObject _unite, Vector3 _posDepart, Vector3 _posArrive, int _prix)
    {
        if(Ressource.instance.UniteEstPayer(_prix))
        {
            if (!Inventaire.instance.NombreUniteMaxAtteint())
            {
                GameObject _obj = Instantiate(_unite, _posDepart, Quaternion.identity);
                _obj.GetComponent<CubeDeplacement>().Deplacer(_posArrive);

                Inventaire.instance.AjouterUniteJoueur();
            }
        }
    }
}


