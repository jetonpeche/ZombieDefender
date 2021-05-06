using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause = null;
    [SerializeField] GameObject controle = null;

    private void Start()
    {
        menuPause.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
            BouttonMenuPause();
    }

    public void Reprendre()
    {
        BouttonMenuPause();
    }

    public void VoirCacherControle(bool _etat)
    {
        Menu.VoirCacherControle(controle, _etat);
    }

    public void MenuPrincipal()
    {
        Menu.MenuPrincipal();
    }

    public void Quitter()
    {
        Menu.Quitter();
    }

    private void BouttonMenuPause()
    {
        Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        AudioListener.pause = !AudioListener.pause;
        menuPause.SetActive(!menuPause.activeSelf);
    }
}
