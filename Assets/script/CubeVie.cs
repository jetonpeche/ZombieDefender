using UnityEngine;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private CubeBarVie cubeBarVie;

    private int vie;

    void Start()
    {
        vie = cubeBarVie.GetVie();
    }

    public void SubirDegat(int _degats)
    {
        vie -= _degats;
        cubeBarVie.SetVieSlider(vie);

        if(!EstEnVie())
        {
            vie = 0;
            Mort();
        }
    }

    public bool EstEnVie()
    {
        return vie > 0;
    }

    private void Mort()
    {
        GetComponent<MeshRenderer>().material.color = Color.black;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.layer = 0;
        gameObject.tag = "Untagged";

        Debug.Log("Mort !");
    }
}
