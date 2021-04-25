using UnityEngine;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private bool estEnnemi;

    [SerializeField] private CubeBarVie cubeBarVie;

    [Header("Resistance de la base en % (10% = 1.1 / -10% = 0.9)")]
    [SerializeField] [Range(1f, 1.9f)] private float multiplicateurResistanceBase = 1.3f;

    [Header("Resistance en % (10% = 1.1 / -10% = 0.9)")]
    [SerializeField] private string tagResistance;
    [SerializeField] [Range(0f, 1f)] private float multiplicateurDegatMoins = 0.5f;

    [Header("faiblesse en % (10% = 1.1 / -10% = 0.9)")]
    [SerializeField] private string tagFaiblesse;
    [SerializeField] [Range(1f, 2f)] private float multiplicateurDegatSupp = 1.5f;

    private int vie;

    void Start()
    {
        vie = cubeBarVie.GetVie();
    }

    public bool EstVivant()
    {
        return vie > 0;
    }

    public void SubirDegat(int _degats, GameObject _cible)
    {
        vie -= CalculDegats(_degats, _cible);
        cubeBarVie.SetVieSlider(vie);

        if(vie <= 0)
        {
            vie = 0;
            Mort();
        }

        // se defendre contre IA ennemi
        if(_cible.GetComponent<CubeDeplacementEnnemi>())
            transform.GetComponentInChildren<CubeAttaque>().Repliquer(_cible);
    }

    public void SubirDegatObjectif(int _degats)
    {
        if(multiplicateurResistanceBase > 1)
        {
            float _pourcentage = 2 - multiplicateurResistanceBase;

            _degats = (int)(_degats * _pourcentage);
        }

        vie -= _degats;
        cubeBarVie.SetVieSlider(vie);

        if(vie <= 0)
        {
            Objectif.objectifDetruit = true;
            Destroy(gameObject);
        }
    }

    private void Mort()
    {
        if (estEnnemi)
            Inventaire.instance.ReduireUniteEnnemi();
        else
            Inventaire.instance.ReduireUniteJoueur();

        Destroy(gameObject);
    }

    private int CalculDegats(int _degats, GameObject _cible)
    {
        if (tagFaiblesse != "" || tagResistance != "")
        {
            if (Tag.PossedeTag(tagFaiblesse, _cible))
            {
                return _degats = (int)(_degats * multiplicateurDegatSupp);
            }

            else if (Tag.PossedeTag(tagResistance, _cible))
            {
                return _degats = (int)(_degats * multiplicateurDegatMoins);
            }
        }

        return _degats;
    }
}
