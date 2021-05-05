using UnityEngine;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private Ragdoll ragdoll;
    [SerializeField] private Arme[] listeArme;
    [SerializeField] private bool estEnnemi;

    [SerializeField] private CubeBarVie cubeBarVie;
    [SerializeField] private AudioSource audioSource;

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
            Mort(_cible.GetComponentInChildren<Arme>());
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

    private void Mort(Arme _armeEnnemi)
    {
        if (estEnnemi)
        {
            SonMort.instance.JouerSonMortFlood(audioSource);
            Inventaire.instance.ReduireUniteEnnemi();
        }
        else
        {
            if (!Tag.PossedeTag("scorpion", gameObject))
                SonMort.instance.JouerSonMortMarine(audioSource);
            else
                SonMort.instance.JouerSonMortScorpion(audioSource);

            Inventaire.instance.ReduireUniteJoueur();
        }

        GetComponentInChildren<CubeAttaque>().enabled = false;
        GetComponentInChildren<ZoneDectection>().enabled = false;


        // scorpion n'en a pas
        if (ragdoll != null)
        {
            foreach (Arme _arme in listeArme)
            {
                _arme.DetacherArme();
            }

            ragdoll.ActiverRagDoll(true);

            if(_armeEnnemi.EstArmeExplosive())
            {
                ragdoll.ForceExplosion(_armeEnnemi.GetForceExplosion(), _armeEnnemi.GetRayonExplosion(), transform.position);
            }

            Destroy(gameObject, 5f);
        }
        else
        {
            GetComponent<MortVehicule>().Mort();
            GetComponent<CubeDeplacement>().BloquerTourelle();
            transform.gameObject.layer = 0;

            Destroy(gameObject, 2f);
        }
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
