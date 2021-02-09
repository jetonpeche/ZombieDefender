using UnityEngine;
using UnityEngine.AI;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private CubeBarVie cubeBarVie;

    private int vie;

    void Start()
    {
        vie = cubeBarVie.GetVie();
    }

    public void SubirDegat(int _degats, GameObject _cible)
    {
        vie -= _degats;
        cubeBarVie.SetVieSlider(vie);

        if(vie <= 0)
        {
            vie = 0;
            Mort();
        }

        // se defendre
        else if(_cible != null)        
            GetComponent<CubeAttaque>().Repliquer(_cible);
    }

    public void SubirDegatObjectif(int _degats)
    {
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
        GetComponent<MeshRenderer>().material.color = Color.black;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

        if(GetComponent<CubeDeplacement>())
        {
            gameObject.GetComponent<CubeClick>().Clack();
            Click.instance.UniteMorte(gameObject);
        }

        GetComponent<Collider>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;

        Destroy(GetComponent<CubeAttaque>());
        Destroy(GetComponentInChildren<ZoneDectection>());
        Destroy(GetComponent<CubeDeplacement>());

        gameObject.layer = 0;
        gameObject.tag = "Untagged";

        Destroy(gameObject, 5);
    }
}
