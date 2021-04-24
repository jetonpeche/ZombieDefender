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

    public void InstancierBPcaserne()
    {
        InstancierBP(bp_caserne);
    }

    public void InstancierBPgarage()
    {
        InstancierBP(bp_garage);
    }

    private void InstancierBP(GameObject _obj)
    {
        Instantiate(_obj);
        MenuBatiment.instance.BouttonMenu();
        MenuBatiment.instance.instanceBP = true;
    }
}
