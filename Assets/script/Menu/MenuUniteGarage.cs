using UnityEngine;

public class MenuUniteGarage : MonoBehaviour
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
        menu.piData[0].onSlicePressed.AddListener(delegate { CreerUnite.instance.InstancierUniteScorpion(pos.posDepart.position, pos.posArrive.position); });

        menu.UpdatePiUI();
    }

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

        GetComponent<Collider>().enabled = !menuOuvert;
    }
}
