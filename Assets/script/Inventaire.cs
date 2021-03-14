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

    [SerializeField] private int nbUniteMaxJoueur;

    private int nbUniteJoueur = 0;

    private void Start()
    {
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité vivante: ", " / " + nbUniteMaxJoueur);
    }

    public void AjouterUniteJoueur()
    {
        nbUniteJoueur++;
        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité: ", " / " + nbUniteMaxJoueur);
    }

    public void ReduireUniteJoueur()
    {
        nbUniteJoueur--;

        if (nbUniteJoueur < 0)
            nbUniteJoueur = 0;

        AfficherNbUnite(txtNbUniteJoueur, nbUniteJoueur, "Unité: ", " / " + nbUniteMaxJoueur);
    }

    public bool NombreUniteMaxAtteint()
    {
        return nbUniteJoueur < nbUniteMaxJoueur; 
    }

    private void AfficherNbUnite(Text _txtUnite, int _nbUnite, string _text, string _max = null)
    {
        _txtUnite.text = _text + _nbUnite.ToString() + _max;
    }
}
