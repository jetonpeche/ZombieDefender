using UnityEngine;

public static class MenuUniteOuvert
{
    public static bool menuOuvert = false;

    public static bool ActiverMenu(PiUIManager _piUI, string _nomMenu)
    {
        _piUI.ChangeMenuState(_nomMenu, new Vector2(Screen.width / 2f, Screen.height / 2f));
        menuOuvert = !menuOuvert;
        DeplacementCamera.instance.FreezeDeplacementCamera();

        return menuOuvert;
    }

}
