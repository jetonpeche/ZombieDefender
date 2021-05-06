using UnityEngine;
using UnityEngine.SceneManagement;

public static class Menu
{
    public static void Jouer()
    {
        SceneManager.LoadScene("jeu");
    }

    public static void MenuPrincipal()
    {
        Time.timeScale = 1;
        AudioListener.pause = false;
        SceneManager.LoadScene("menuPrincipal");
    }

    public static void VoirCacherControle(GameObject _menu, bool _etat)
    {
        _menu.SetActive(_etat);
    }

    public static void Quitter()
    {
        Application.Quit();
    }
}
