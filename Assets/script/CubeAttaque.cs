using UnityEngine;

public class CubeAttaque : MonoBehaviour
{

    [HideInInspector] public GameObject cible;
    [SerializeField] private ZoneDectection zoneDectection;

    public void Attaquer()
    {
        CubeVie _cubeViePasGentil = cible.GetComponent<CubeVie>();

        if (_cubeViePasGentil.EstEnVie())
        {
            _cubeViePasGentil.SubirDegat(20);
            Debug.Log(cible.name);
        }          
        else
        {
            cible = null;
            CancelInvoke("Attaquer");
            zoneDectection.BouttonActiverCibleAuto(true);
        }
    }
}
