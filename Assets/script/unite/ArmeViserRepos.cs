using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(RigBuilder))]
public class ArmeViserRepos : MonoBehaviour
{
    [SerializeField] private float vitesseVise;
    [SerializeField] private Rig arme_viser;

    private bool viser, repos;

    void Start()
    {
        arme_viser.weight = 0;
    }

    void Update()
    {
        if(viser)
        {
            if(arme_viser.weight < 1)
            {
                arme_viser.weight += Time.deltaTime / vitesseVise;
            }
        }
        else if(repos)
        {
            arme_viser.weight -= Time.deltaTime / vitesseVise;
        }
    }

    public void Viser()
    {
        viser = true;
        repos = false;
    }

    public void Repos()
    {
        viser = false;
        repos = true;
    }
}
