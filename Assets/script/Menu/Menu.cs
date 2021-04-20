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
        SceneManager.LoadScene("menuPrincipal");
    }

    public static void Quitter()
    {
        Application.Quit();
    }
}
