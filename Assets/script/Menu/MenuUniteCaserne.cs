using UnityEngine;

public class MenuUniteCaserne : MonoBehaviour
{
    [SerializeField] private PiUIManager piUi = null; 
    [SerializeField] private string nomMenu = null;

    private PiUI menu;
    private bool menuOuvert = false;
    private Position pos;

    private void Awake()
    {
        pos = GetComponent<Position>();
        piUi = GameObject.Find("Pi UI Canvas").GetComponent<PiUIManager>();
        menu = piUi.GetPiUIOf(nomMenu);
    }

    private void Start()
    {
        // ajout des fonctions aux events du menu
        // delegate { } pour accepter les parametres dans le AddListerner()
        menu.piData[0].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteMarine(pos.posDepart.position, pos.posArrive.position); });
        menu.piData[1].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteSniper(pos.posDepart.position, pos.posArrive.position); });
        menu.piData[2].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteSpnkr(pos.posDepart.position, pos.posArrive.position); });

        menu.UpdatePiUI();
    }

    private void OnMouseDown()
    {
        if (!MenuUniteOuvert.menuOuvert)
            ActiverMenu();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && MenuUniteOuvert.menuOuvert && menuOuvert)
        {
            ActiverMenu();
        }
    }

    private void ActiverMenu()
    {
        menuOuvert = MenuUniteOuvert.ActiverMenu(piUi, menu.name);
        GetComponent<Collider>().enabled = !menuOuvert;
    }
}
