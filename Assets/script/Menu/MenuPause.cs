using UnityEngine;

public class MenuPause : MonoBehaviour
{
    [SerializeField] GameObject menuPause = null;

    private void Start()
    {
        menuPause.SetActive(false);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            BouttonMenuPause();
        }
    }

    public void Reprendre()
    {
        BouttonMenuPause();
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
        menuPause.SetActive(!menuPause.activeSelf);
    }
}
