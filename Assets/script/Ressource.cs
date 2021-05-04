using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Ressource : MonoBehaviour
{
    #region Sinletone
    public static Ressource instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private int nbGenerationRessource;
    [SerializeField] private int ressourceTotal;

    [SerializeField][Min(1)] private float vitesseGenerationRessource;

    [SerializeField] private Text txtNbRessource = null;
    [SerializeField] private Text txtPasRessource = null;

    private void Start()
    {
        txtNbRessource.text = ressourceTotal.ToString();

        StartCoroutine(GenererRessource());
    }

    public bool UniteEstPayer(int _prix)
    {
        if (ressourceTotal - _prix >= 0)
        {
            ressourceTotal -= _prix;
            return true;
        }
        else
        {
            if(!txtPasRessource.enabled)
                StartCoroutine(AfficherText());

            return false;
        }
    }

    private IEnumerator GenererRessource()
    {
        while(!Objectif.objectifDetruit)
        {
            yield return new WaitForSeconds(vitesseGenerationRessource);

            ressourceTotal += nbGenerationRessource;
            txtNbRessource.text = ressourceTotal.ToString();
        }
    }

    private IEnumerator AfficherText()
    {
        txtPasRessource.enabled = true;
        yield return new WaitForSeconds(2);
        txtPasRessource.enabled = false;
    }
}
