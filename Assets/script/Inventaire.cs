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

    [SerializeField] private Text txtNbUniteJoueur = null;
    [SerializeField] private Text txtNbUniteEnnemi = null;
    [SerializeField] private Text txtNbManche = null;

    [SerializeField] private int nbUniteMaxJoueur;
    [SerializeField] private int nbUniteMaxEnnemi;

    private int nbUniteJoueur = 0;
    private int nbUniteEnnemi = 0;

    private int manche = 1;

    private void Start()
    {
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité: ", " / " + nbUniteMaxJoueur);
        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unité ennemi: ");
        txtNbManche.text = "Manche: " + manche;
    }

    public void AjouterUniteJoueur()
    {
        nbUniteJoueur++;
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité: ", " / " + nbUniteMaxJoueur);
    }

    public void AjouterUniteEnnemi()
    {
        nbUniteEnnemi++;
        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unité ennemi: ");
    }

    public void ReduireUniteJoueur()
    {
        nbUniteJoueur--;

        if (nbUniteJoueur < 0)
            nbUniteJoueur = 0;

        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité: ", " / " + nbUniteMaxJoueur);
    }

    public void ReduireUniteEnnemi()
    {
        nbUniteEnnemi--;

        AfficherNbUnite(txtNbUniteEnnemi, nbUniteEnnemi, "Unité ennemi: ");
    }

    public void MancheTermine()
    {
        manche++;
        txtNbManche.text = "Manche: " + manche;
    }

    public bool NombreUniteMaxAtteint()
    {
        return nbUniteJoueur > nbUniteMaxJoueur; 
    }

    public bool NombreUniteMaxEnnemiAtteint()
    {
        return nbUniteEnnemi > nbUniteMaxEnnemi;
    }

    private void AfficherNbUnite(Text _txtUnite, int _nbUnite, string _text, string _max = null)
    {
        _txtUnite.text = _text + _nbUnite.ToString() + _max;
    }
}
