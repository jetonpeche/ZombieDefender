using UnityEngine;

public class MenuUniteCaserne : MonoBehaviour
{
    [SerializeField] private string labelUniteMarine;
    [SerializeField] private int prixUniteMarine;

    [SerializeField] private string labelUniteSniper;
    [SerializeField] private int prixUniteSniper;

    [SerializeField] private string labelUniteSpnkr;
    [SerializeField] private int prixUniteSpnkr;

    [Header("")]
    [SerializeField] private string nomMenu = null;

    private PiUIManager piUi = null;
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
        menu.piData[0].sliceLabel = labelUniteMarine + " (" + prixUniteMarine + ")";
        menu.piData[0].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteMarine(pos.posDepart.position, pos.posArrive.position, prixUniteMarine); });

        menu.piData[1].sliceLabel = labelUniteSniper + " (" + prixUniteSniper + ")";
        menu.piData[1].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteSniper(pos.posDepart.position, pos.posArrive.position, prixUniteSniper); });

        menu.piData[2].sliceLabel = labelUniteSpnkr + " (" + prixUniteSpnkr + ")";
        menu.piData[2].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteSpnkr(pos.posDepart.position, pos.posArrive.position, prixUniteSpnkr); });

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
