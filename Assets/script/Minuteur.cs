using UnityEngine;
using UnityEngine.UI;

public class Minuteur : MonoBehaviour
{
    #region Singletoon
    public static Minuteur instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private GameObject minuteur = null;
    [SerializeField] private Text txtMinuteur = null;

    [SerializeField] [Min(1)] private float tempsEntreDeuxVague = 30;

    private float tempsEntreDeuxVagueClone;
    private bool pauseEntreDeuxVague;

    private void Start()
    {
        tempsEntreDeuxVagueClone = tempsEntreDeuxVague;

        DemarerMinuteur();
    }

    private void Update()
    {
        // passer la pause
        if(Input.GetKeyDown(KeyCode.F3) && pauseEntreDeuxVague)
            FinPause();

        if (pauseEntreDeuxVague)
        {
            tempsEntreDeuxVague -= Time.deltaTime;
            txtMinuteur.text = tempsEntreDeuxVague.ToString("00:00");

            if (tempsEntreDeuxVague <= 0)
                FinPause();
        }
        else
        {
            SonFond.instance.JouerMusic();
        }
    }

    public void DemarerMinuteur()
    {
        pauseEntreDeuxVague = true;
        minuteur.SetActive(true);
    }

    public bool GetPauseEntreDeuxVague()
    {
        return pauseEntreDeuxVague;
    }

    private void FinPause()
    {
        tempsEntreDeuxVague = tempsEntreDeuxVagueClone;
        pauseEntreDeuxVague = false;
        minuteur.SetActive(false);

        Inventaire.instance.NouvelleManche();
    }
}
