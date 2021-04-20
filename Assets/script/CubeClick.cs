using UnityEngine;

public class CubeClick : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    public void OnMouseOver()
    {
        if(canvas != null)
            BouttonMontrerBarVie(true);

        Click();
    }

    public void OnMouseExit()
    {
        if (canvas != null)
            BouttonMontrerBarVie(false);

        Clack();
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
