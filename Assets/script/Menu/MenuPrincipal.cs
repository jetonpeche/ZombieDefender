using UnityEngine;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject menuControle = null;

    public void Jouer()
    {
        Menu.Jouer();
    }

    public void VoirControle(bool _etat)
    {
        Menu.VoirCacherControle(menuControle, _etat);
    }

    public void Quitter()
    {
        Menu.Quitter();
    }
}
