using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private int degats;

    private string tag;
    private Vector3 posDepart;
    private float distance;

    private void Update()
    {
        // ne pas aller plus loin que la porter
        if(Vector3.Distance(transform.position, posDepart) > distance)
            Destroy(gameObject);
    }

    public void Initialiser(Vector3 _posDepart, float _distance, string _tag)
    {
        posDepart = _posDepart;
        distance = _distance;
        tag = _tag;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(tag))
        {
            other.GetComponent<CubeVie>().SubirDegat(degats);
            Destroy(gameObject);
        }   
    }
}
