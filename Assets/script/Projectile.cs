using UnityEngine;

public class Projectile : MonoBehaviour
{
    private int degats;
    private string tagCible;
    private Vector3 posDepart;
    private float distance;
    private GameObject ennemi;
    private float vitesse;

    private void Update()
    {
        // ne pas aller plus loin que la porter
        if(Vector3.Distance(transform.position, posDepart) > distance)
            Destroy(gameObject);
    }

    public void Initialiser(Vector3 _posDepart, float _distance, string _tagCible, int _degats, GameObject _ennemi, float _vitesse)
    {
        posDepart = _posDepart;
        distance = _distance;
        tagCible = _tagCible;
        ennemi = _ennemi;
        degats = _degats;
        vitesse = _vitesse;

        GetComponent<Rigidbody>().velocity = transform.forward * vitesse;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Tag.PossedeTag(tagCible, other.gameObject))
        {
            // ennemi
            if(other.GetComponent<CubeAttaque>())
            {
                other.GetComponent<CubeVie>().SubirDegat(degats, ennemi);
            }
            // objectif
            else
            {
                other.GetComponent<CubeVie>().SubirDegatObjectif(degats);
            }
            
            Destroy(gameObject);
        }   
        else if(other.CompareTag("mur"))
        {
            Destroy(gameObject);
        }
    }
}
