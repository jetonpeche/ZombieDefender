using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUnite : MonoBehaviour
{
    [SerializeField] private PiUIManager piUi = null;
    [SerializeField] private PiUI menu = null;

    private bool menuOuvert = false;

    private void OnMouseDown()
    {
        ActiverMenu();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && menuOuvert)
        {
            ActiverMenu();
        }
    }

    private void ActiverMenu()
    {
        piUi.ChangeMenuState(menu.name, new Vector2(Screen.width / 2f, Screen.height / 2f));
        menuOuvert = !menuOuvert;
        DeplacementCamera.instance.FreezeDeplacementCamera();
    }
}
