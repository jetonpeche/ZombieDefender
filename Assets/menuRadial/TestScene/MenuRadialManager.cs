using UnityEngine;

public class MenuRadialManager : MonoBehaviour
{

    [SerializeField]
    PiUIManager piUi;
    private PiUI normalMenu;
    // Use this for initialization
    void Start()
    {
        // le nom du menu
        normalMenu = piUi.GetPiUIOf("Normal Menu");
    }

    void Update()
    {
        // ouvrir et ferme le menu
        if (Input.GetKeyDown(KeyCode.Space))
        {
            piUi.ChangeMenuState("Normal Menu", new Vector2(Screen.width / 2f, Screen.height / 2f));
        }
    }
}
