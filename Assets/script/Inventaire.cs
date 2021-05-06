using UnityEngine;
using UnityEngine.UI;

public class Inventaire : MonoBehaviour
{
    #region singletoon
    public static Inventaire instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    #region variables

    [SerializeField] private int nbUniteFaireSpawn = 10;

    [SerializeField] private Text txtNbUniteJoueur = null;
    [SerializeField] private Text txtNbUniteEnnemi = null;
    [SerializeField] private Text txtNbManche = null;
    [SerializeField] private Text txtNbEnnemisTuer = null;

    [SerializeField] private int nbUniteMaxJoueur;
    [SerializeField] private int nbUniteMaxEnnemi;
    
    // unités sur le terrain
    private int nbUniteJoueur = 4;
    private int nbUniteEnnemi = 0;

    private int nbUniteEnnemiSpawner = 0;
    private int nbUniteTuer = 0;

    private int manche = 0;

    #endregion

    private void Start()
    {
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unités: ", " / " + nbUniteMaxJoueur);
        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unités ennemi: ");
        AfficherNbUnite(txtNbEnnemisTuer, nbUniteTuer, "Unités tuées: ");
        txtNbManche.text = "Manche: " + manche;
    }

    #region Fonctions void

    public void AjouterUniteJoueur()
    {
        nbUniteJoueur++;
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unités: ", " / " + nbUniteMaxJoueur);
    }

    public void AjouterUniteEnnemi()
    {
        nbUniteEnnemi++;
        nbUniteEnnemiSpawner++;
        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unités ennemi: ");
    }

    public void ReduireUniteJoueur()
    {
        nbUniteJoueur--;

        if (nbUniteJoueur < 0)
            nbUniteJoueur = 0;

        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unités: ", " / " + nbUniteMaxJoueur);
    }

    public void ReduireUniteEnnemi()
    {
        nbUniteEnnemi--;
        nbUniteTuer++;

        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unités ennemi: ");
        AfficherNbUnite(txtNbEnnemisTuer, nbUniteTuer, "Unités tuées: ");
    }

    public void MancheTerminer()
    {
        SonFond.instance.StopperMusic();
        Minuteur.instance.DemarerMinuteur();
    }

    public void NouvelleManche()
    {
        SonFond.instance.JouerSonHorde();

        manche++;
        txtNbManche.text = "Manche: " + manche;

        nbUniteFaireSpawn = (nbUniteEnnemiSpawner + nbUniteEnnemiSpawner) + nbUniteFaireSpawn / 2;
        nbUniteEnnemiSpawner = 0;
    }

    #endregion

    #region Fonctions bool

    public bool NombreUniteMaxAtteint()
    {
        return nbUniteJoueur >= nbUniteMaxJoueur; 
    }

    public bool NombreUniteMaxEnnemiAtteint()
    {
        return nbUniteEnnemi >= nbUniteMaxEnnemi;
    }

    public bool NombreEnnemisSpawnAtteint()
    {
        return nbUniteFaireSpawn == nbUniteEnnemiSpawner;
    }

    public bool MancheEstTerminer()
    {
        return nbUniteEnnemi == 0 && nbUniteEnnemiSpawner == nbUniteFaireSpawn;
    }

    private void AfficherNbUnite(Text _txtUnite, int _nbUnite, string _text, string _max = null)
    {
        _txtUnite.text = _text + _nbUnite.ToString() + _max;
    }

    #endregion
}
