using UnityEngine;

public class SonUnite : MonoBehaviour
{
    #region Singleton
    public static SonUnite instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private AudioSource audioSource;

    [Header("marines")]
    [SerializeField] private string tagAR = "ar";
    [SerializeField] private string tagSPNKr = "spnkr";
    [SerializeField] private AudioClip[] listeSonSelectionMarine;
    [SerializeField] private AudioClip[] listeSonDeplacementMarine;

    [Header("sniper")]
    [SerializeField] private string tagSniper = "sniper";
    [SerializeField] private AudioClip[] listeSonSelectionSniper;
    [SerializeField] private AudioClip[] listeSonDeplacementSniper;

    [Header("scorpion")]
    [SerializeField] private string tagScorpion = "scorpion";
    [SerializeField] private AudioClip[] listeSonSelectionScorpion;
    [SerializeField] private AudioClip[] listeSonDeplacementScorpion;

    public void JouerSonUniteSelection(GameObject _unite)
    {
        if (Tag.PossedeTag(tagAR, _unite) || Tag.PossedeTag(tagSPNKr, _unite))
        {
            JouerSon(listeSonSelectionMarine);
        }
        else if (Tag.PossedeTag(tagSniper, _unite))
        {
            JouerSon(listeSonSelectionSniper);
        }
        else
        {
            JouerSon(listeSonSelectionScorpion);
        }
    }

    public void JouerSonUniteDeplacement(GameObject _unite)
    {
        if (Tag.PossedeTag(tagAR, _unite) || Tag.PossedeTag(tagSPNKr, _unite))
        {
            JouerSon(listeSonDeplacementMarine);
        }
        else if (Tag.PossedeTag(tagSniper, _unite))
        {
            JouerSon(listeSonDeplacementSniper);
        }
        else
        {
            JouerSon(listeSonDeplacementScorpion);
        }
    }

    private void JouerSon(AudioClip[] _list)
    {
        audioSource.PlayOneShot(_list[Random.Range(0, _list.Length)]);
    }
}
