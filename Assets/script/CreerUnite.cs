using UnityEngine;

public class CreerUnite : MonoBehaviour
{
    public GameObject[] tabUnite;
    public Transform posSpawn;
    public Transform posAatteindre;

    public void CreerUnitePistolet()
    {
        GameObject _obj = Instantiate(tabUnite[0], posSpawn.position, Quaternion.identity);
        _obj.GetComponent<CubeDeplacement>().Deplacer(posAatteindre.position);
    }

    public void CreerUniteSniper()
    {
        GameObject _obj = Instantiate(tabUnite[1], posSpawn.position, Quaternion.identity);
        _obj.GetComponent<CubeDeplacement>().Deplacer(posAatteindre.position);
    }
}


