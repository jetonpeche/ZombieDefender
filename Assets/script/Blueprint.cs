using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [SerializeField] private LayerMask terrain;
    [SerializeField] private GameObject prefab;

    private RaycastHit hit;
    private PiUI piUI;

    private void Awake()
    {
        piUI = GameObject.Find("Menu batiment").GetComponent<PiUI>();
    }

    private void Update()
    {
       Ray _rayon = Camera.main.ScreenPointToRay(Input.mousePosition);

        if(Physics.Raycast(_rayon, out hit, Mathf.Infinity, terrain))
        {
            transform.position = hit.point;
        }

        // creer batiment
        if(Input.GetMouseButtonDown(0))
        {
            Instantiate(prefab, hit.point, Quaternion.identity);

            // desactive le boutton du menu du batiment
            foreach (PiUI.PiData item in piUI.piData)
            {
                if(item.sliceLabel == prefab.name)
                {
                    item.isInteractable = false;
                    item.SetValues(item);
                    piUI.UpdatePiUI();
                    break;
                }
            }

            DetruireObj();
        }

        // annuler
        else if(Input.GetMouseButtonDown(1))
        {
            DetruireObj();
        }
    }

    private void DetruireObj()
    {
        MenuBatiment.instance.instanceBP = false;
        Destroy(gameObject);
    }
}
