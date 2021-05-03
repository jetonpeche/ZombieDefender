using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float rayonExplosion;

    private int degats;
    private string tagCible;
    private Vector3 posDepart;
    private float distance;
    private GameObject ennemi;
    private float vitesse;
    private float dispersionTir;
    private LayerMask layersCible;

    private bool estExplosif;

    private void Update()
    {
        // ne pas aller plus loin que la porter
        if(Vector3.Distance(transform.position, posDepart) > distance)
            Destroy(gameObject);
    }

    public void Initialiser(Vector3 _posDepart, float _distance, string _tagCible, int _degats, GameObject _ennemi, float _vitesse, float _dispersionTir, LayerMask _layer, bool _estExplosif)
    {
        layersCible = _layer;
        estExplosif = _estExplosif;
        posDepart = _posDepart;
        distance = _distance;
        tagCible = _tagCible;
        ennemi = _ennemi;
        degats = _degats;
        vitesse = _vitesse;
        dispersionTir = Random.Range(-_dispersionTir, _dispersionTir);

        GetComponent<Rigidbody>().velocity = transform.forward + DispertionTir() * vitesse;
    }
    private Vector3 DispertionTir()
    {
        return (transform.forward + (transform.up * dispersionTir) + (transform.right * Random.Range(-1, 1) * dispersionTir)).normalized;
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (Tag.PossedeTag(tagCible, _other.gameObject))
        {
            CubeVie _cubeVie = _other.GetComponent<CubeVie>();               

            // ennemi
            if (_other.GetComponentInChildren<CubeAttaque>())
            {
                if(estExplosif)
                {
                    Collider[] _listeEnnemi = Physics.OverlapSphere(transform.position, rayonExplosion, layersCible);

                    foreach (Collider _col in _listeEnnemi)
                    {
                        _cubeVie = _col.gameObject.GetComponent<CubeVie>();

                        if (_cubeVie.EstVivant())
                            _cubeVie.SubirDegat(degats, ennemi);
                    }
                }
                else
                {
                    if (_cubeVie.EstVivant())
                        _cubeVie.SubirDegat(degats, ennemi);
                }  
            }
            // objectif
            else
            {
                _cubeVie.SubirDegatObjectif(degats);
            }
        }

        Destroy(gameObject);
    }

    public float GetRayonExplosion()
    {
        return rayonExplosion;
    }
}
