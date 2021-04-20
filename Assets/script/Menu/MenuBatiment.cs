using UnityEngine;

public class MenuBatiment : MonoBehaviour
{
    #region singletoon
    public static MenuBatiment instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private PiUIManager piUi;
    [SerializeField] private PiUI menu;

    public bool instanceBP;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !instanceBP)
        {
            BouttonMenu();
        }
    }

    public void BouttonMenu()
    {
        piUi.ChangeMenuState(menu.name, new Vector2(Screen.width / 2f, Screen.height / 2f));
    }
}
