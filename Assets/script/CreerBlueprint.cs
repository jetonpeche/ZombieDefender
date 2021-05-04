using UnityEngine;

public class CreerBlueprint : MonoBehaviour
{
    #region Singletoon
    public static CreerBlueprint instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject bp_caserne = null;
    [SerializeField] private GameObject bp_garage = null;

    public void InstancierBPcaserne(int _prix)
    {
        InstancierBP(bp_caserne, _prix);
    }

    public void InstancierBPgarage(int _prix)
    {
        InstancierBP(bp_garage, _prix);
    }

    private void InstancierBP(GameObject _obj, int _prix)
    {
        if(Ressource.instance.UniteEstPayer(_prix))
        {
            Instantiate(_obj);
            MenuBatiment.instance.BouttonMenu();
            MenuBatiment.instance.instanceBP = true;
        }
    }
}
