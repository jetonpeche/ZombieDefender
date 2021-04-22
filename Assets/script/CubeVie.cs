using UnityEngine;
using UnityEngine.AI;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private CubeBarVie cubeBarVie;

    [Header("Multiplicateur en % (10% = 1.1 / -10% = 0.9)")]
    [SerializeField] [Range(0f, 2f)] private float multiplicateurResistanceBase = 1.3f;

    [SerializeField] private string tagResistance;
    [SerializeField] [Range(0f, 2f)] private float multiplicateurDegatMoins = 0.5f;

    [SerializeField] private string tagFaiblesse;
    [SerializeField] [Range(0f, 2f)] private float multiplicateurDegatSupp = 1.5f;

    private int vie;

    void Start()
    {
        vie = cubeBarVie.GetVie();
    }

    public void SubirDegat(int _degats, GameObject _cible)
    {
        if(tagFaiblesse == "" || tagResistance == "")
        {
            if (Tag.PossedeTag(tagFaiblesse, _cible))
            {
                _degats = (int)(_degats * multiplicateurDegatSupp);
            }
            else if (Tag.PossedeTag(tagResistance, _cible))
            {
                _degats = (int)(_degats * multiplicateurDegatMoins);
            }
        }

        vie -= _degats;
        cubeBarVie.SetVieSlider(vie);

        if(vie <= 0)
        {
            vie = 0;
            Mort();
        }

        // se defendre contre IA ennemi
        if(_cible.GetComponent<CubeDeplacementEnnemi>())
            GetComponent<CubeAttaque>().Repliquer(_cible);         
    }

    public void SubirDegatObjectif(int _degats)
    {
        if(multiplicateurResistanceBase > 0)
             _degats = (int)(_degats * multiplicateurResistanceBase);           

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
        // unite joueur
        if(GetComponent<CubeDeplacement>())
        {
            gameObject.GetComponent<CubeClick>().Clack();
            Click.instance.UniteMorte(gameObject);
            Destroy(GetComponent<CubeDeplacement>());

            Inventaire.instance.ReduireUniteJoueur();
        }
        else
        {
            Destroy(GetComponent<CubeDeplacementEnnemi>());
            Inventaire.instance.ReduireUniteEnnemi();
        }   

        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        Destroy(GetComponent<CubeAttaque>());
        Destroy(GetComponentInChildren<ZoneDectection>());

        gameObject.layer = 2;
        gameObject.tag = "Untagged";

        Destroy(gameObject, 5);
    }
}
