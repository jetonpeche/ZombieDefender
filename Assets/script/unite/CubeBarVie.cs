using UnityEngine;
using UnityEngine.UI;

public class CubeBarVie : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private int maxVie;
    [SerializeField] GameObject canvas;
    private int vie;

    private void Update()
    {
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
    }

    private void Awake()
    {
        slider.value = slider.maxValue = maxVie;
        vie = maxVie;
    }

    public void SetVieSlider(int _vie)
    {
        slider.value = _vie;
    }

    public int GetVie()
    {
        return vie;
    }
}
