using UnityEngine;

public class CubeVie : MonoBehaviour
{
    [SerializeField] private int vieMax;
    private int vie;

    void Start()
    {
        vie = vieMax;
    }

    public void SubirDegat(int _degats)
    {
        vie -= _degats;

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

    public void Mort()
    {
        GetComponent<MeshRenderer>().material.color = Color.black;
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        gameObject.layer = 0;

        Debug.Log("Mort !");
    }
}
