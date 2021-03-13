using UnityEngine;

public class CreerUnite : MonoBehaviour
{
    [SerializeField] private Transform posSpawn;
    [SerializeField] private Transform posAatteindre;

    public void InstancierUnite(GameObject _unite)
    {
        if(Inventaire.instance.NombreUniteMaxAtteint())
        {
            GameObject _obj = Instantiate(_unite, posSpawn.position, Quaternion.identity);
            _obj.GetComponent<CubeDeplacement>().Deplacer(posAatteindre.position);

            Inventaire.instance.AjouterUniteJoueur();
        }
    }
}


