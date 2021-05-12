using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPrincipal : MonoBehaviour
{
    [SerializeField] private GameObject menuControle = null;

    [SerializeField] private Slider slider = null;
    [SerializeField] private Text txtPourcentageChargement = null;
    [SerializeField] private GameObject ecranChargement = null;

    public void Jouer()
    {
        StartCoroutine(ChargerScene());
    }

    public void VoirControle(bool _etat)
    {
        Menu.VoirCacherControle(menuControle, _etat);
    }

    public void Quitter()
    {
        Menu.Quitter();
    }

    private IEnumerator ChargerScene()
    {
        AsyncOperation _operation = SceneManager.LoadSceneAsync("jeu");
        ecranChargement.SetActive(true);

        while(!_operation.isDone)
        {
            float _progressionChargement = Mathf.Clamp01(_operation.progress / 0.9f);
            slider.value = _progressionChargement;
            txtPourcentageChargement.text = _progressionChargement * 100f + "%";

            yield return null;
        }
    }
}
