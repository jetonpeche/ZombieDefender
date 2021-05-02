using UnityEngine;

public class SonMort : MonoBehaviour
{
    #region Singletone
    public static SonMort instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private AudioClip[] listeSonMortMarine;
    [SerializeField] private AudioClip[] listeSonMortFlood;

    public void JouerSonMortMarine(AudioSource _audioSource)
    {
        JouerSonMort(_audioSource, listeSonMortMarine);
    }

    public void JouerSonMortFlood(AudioSource _audioSource)
    {
        JouerSonMort(_audioSource, listeSonMortFlood);
    }

    private void JouerSonMort(AudioSource _audioSource, AudioClip[] _listeAudioClip)
    {
        _audioSource.PlayOneShot(_listeAudioClip[Random.Range(0, _listeAudioClip.Length)]);
    }
}
