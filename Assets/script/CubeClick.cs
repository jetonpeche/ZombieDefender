using UnityEngine;

public class CubeClick : MonoBehaviour
{
    public bool estSelectionne;

    [SerializeField] private GameObject canvas;

    public void OnMouseOver()
    {
        if(canvas != null)
            BouttonMontrerBarVie(true);

        if(!estSelectionne)
            Click();
    }

    public void OnMouseExit()
    {
        if (canvas != null)
            BouttonMontrerBarVie(false);

        if (!estSelectionne)
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
