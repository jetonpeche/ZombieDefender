using UnityEngine;

public class DeplacementCamera : MonoBehaviour
{
    #region singletoon
    public static DeplacementCamera instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private int vitesseCamera;
    [SerializeField] private float vitesseScroll;

    [SerializeField] private float zoomMin, zoomMax;

    private bool deplacementFreezer = false;
    private Vector3 camPos;

    private void Update()
    {
        if(!deplacementFreezer)
        {
            Deplacement();
            Zoom();
        }
    }

    public void FreezeDeplacementCamera()
    {
        deplacementFreezer = !deplacementFreezer;
    }

    private void Deplacement()
    {
        float _mouvX = Input.GetAxisRaw("Horizontal");
        float _mouvZ = Input.GetAxisRaw("Vertical");

        Vector3 _deplacementX = _mouvX * Vector3.right;
        Vector3 _deplacementY = _mouvZ * Vector3.forward;

        Vector3 _deplacement = (_deplacementX + _deplacementY).normalized * vitesseCamera * Time.deltaTime;

        transform.position += _deplacement;
    }

    private void Zoom()
    {
        camPos = transform.position;

        // scroll souris pour zoomer
        float _scroll = Input.GetAxis("Mouse ScrollWheel") * vitesseScroll;

        camPos.y -= _scroll * 100f * Time.deltaTime;

        camPos.y = Mathf.Clamp(camPos.y, zoomMin, zoomMax);

        transform.position = camPos;
    }
}
