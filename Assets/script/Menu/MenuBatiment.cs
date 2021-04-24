using UnityEngine;

public class MenuBatiment : MonoBehaviour
{
    public static MenuBatiment instance;

    [SerializeField] private PiUIManager piUi;
    [SerializeField] private string nomMenu;

    public PiUI menu;

    public bool instanceBP;

    private void Awake()
    {      
        piUi = GameObject.Find("Pi UI Canvas").GetComponent<PiUIManager>();
        menu = piUi.GetPiUIOf(nomMenu);

        instance = this;
    }

    private void Start()
    {
        // ajout des fonctions aux events du menu
        // delegate { } pour accepter les parametres dans le AddListerner()
        menu.piData[0].onSlicePressed.AddListener(CreerBlueprint.instance.InstancierBPcaserne);
        menu.piData[1].onSlicePressed.AddListener(CreerBlueprint.instance.InstancierBPgarage);

        menu.UpdatePiUI();
    }

    private void OnMouseDown()
    {
        if(!instanceBP)
            BouttonMenu();
    }

    public void BouttonMenu()
    {
        piUi.ChangeMenuState(menu.name, new Vector2(Screen.width / 2f, Screen.height / 2f));
    }
}
