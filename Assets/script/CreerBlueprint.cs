using UnityEngine;

public class CreerBlueprint : MonoBehaviour
{
    public void InstancierBP(GameObject _obj)
    {
        Instantiate(_obj);
        MenuBatiment.instance.BouttonMenu();
        MenuBatiment.instance.instanceBP = true;
    }
}
