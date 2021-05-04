using UnityEngine;

public class MenuBatiment : MonoBehaviour
{
    public static MenuBatiment instance;

    [SerializeField] private string labelNomCaserne;
    [SerializeField] private int prixCaserne;

    [SerializeField] private string labelNomGarage;
    [SerializeField] private int prixGarage;

    [Header("")]
    [SerializeField] private string nomMenu;

    private PiUIManager piUi;
    private PiUI menu;
    private bool menuOuvert = false;

    public bool instanceBP;

    private void Awake()
    {
        piUi = GameObject.Find("Pi UI Canvas").GetComponent<PiUIManager>();
        instance = this;
    }

    private void Start()
    {
        menu = piUi.GetPiUIOf(nomMenu);

        // ajout des fonctions aux events du menu
        menu.piData[0].sliceLabel = labelNomCaserne + " (" + prixCaserne + ")";
        menu.piData[0].onSlicePressed.AddListener(delegate { CreerBlueprint.instance.InstancierBPcaserne(prixCaserne); });

        menu.piData[1].sliceLabel = labelNomGarage + " (" + prixGarage + ")";
        menu.piData[1].onSlicePressed.AddListener(delegate { CreerBlueprint.instance.InstancierBPgarage(prixGarage); });

        menu.UpdatePiUI();
    }

    private void OnMouseDown()
    {
        if(!instanceBP && !MenuUniteOuvert.menuOuvert)
            BouttonMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && MenuUniteOuvert.menuOuvert && menuOuvert)
        {
            BouttonMenu();
        }
    }

    public void BouttonMenu()
    {
       menuOuvert = MenuUniteOuvert.ActiverMenu(piUi, menu.name);
    }
}
