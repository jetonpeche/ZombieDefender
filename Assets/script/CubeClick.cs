using UnityEngine;

public class CubeClick : MonoBehaviour
{
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
