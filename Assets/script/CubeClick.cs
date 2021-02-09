using UnityEngine;

public class CubeClick : MonoBehaviour
{
    [SerializeField] GameObject canvas;
     
    public void OnMouseOver()
    {
        BouttonMontrerBarVie(true);
    }

    public void OnMouseExit()
    {
        BouttonMontrerBarVie(false);
    }

    public void BouttonMontrerBarVie(bool _stat)
    {
        canvas.GetComponent<Canvas>().enabled = _stat;
    }

    public void Click()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = true;
        GetComponentInChildren<Animation>().Play();
    }

    public void Clack()
    {
        GetComponentInChildren<SpriteRenderer>().enabled = false;
        GetComponentInChildren<Animation>().Stop();
    }
}
