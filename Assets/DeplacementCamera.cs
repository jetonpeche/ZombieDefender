using UnityEngine;

public class DeplacementCamera : MonoBehaviour
{
    [SerializeField] private int vitesseCamera;

    // Update is called once per frame
    void Update()
    {
        float mouvX = Input.GetAxisRaw("Horizontal");
        float mouvZ = Input.GetAxisRaw("Vertical");

        Vector3 deplacementX = mouvX * Vector3.right;
        Vector3 deplacementZ = mouvZ * Vector3.forward;

        deplacementZ.y = 0;

        Vector3 deplacement = (deplacementX + deplacementZ).normalized * vitesseCamera * Time.deltaTime;

        transform.position += deplacement;
    }
}
