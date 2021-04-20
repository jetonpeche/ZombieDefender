using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuGameOver : MonoBehaviour
{
    [SerializeField] private GameObject menuGameOver;

    private void Start()
    {
        menuGameOver.SetActive(false);
    }

    private void Update()
    {
        if(Objectif.objectifDetruit)
        {
            Time.timeScale = 0;
            menuGameOver.SetActive(true);
        }
    }

    public void Rejouer()
    {
        Menu.Jouer();
    }

    public void MenuPrincipal()
    {
        Menu.MenuPrincipal();
    }

    public void Quitter()
    {
        Menu.Quitter();
    }
}
