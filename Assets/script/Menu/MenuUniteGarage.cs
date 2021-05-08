using UnityEngine;

public class MenuUniteGarage : MonoBehaviour
{
    [SerializeField] private string labelUniteScorpion;
    [SerializeField] private int prixUniteScorpion;

    [Header("")]
    [SerializeField] private string nomMenu;

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
        menu.piData[0].sliceLabel = labelUniteScorpion + " (" + prixUniteScorpion + ")";
        menu.piData[0].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteScorpion(pos.posDepart.position, pos.posArrive.position, prixUniteScorpion); });

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
