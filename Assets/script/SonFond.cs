using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SonFond : MonoBehaviour
{
    #region Singletoon
    public static SonFond instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    [SerializeField] private AudioClip[] listeSonFloodHord = null;
    [SerializeField] private AudioClip[] listeMusic = null;

    [SerializeField] private AudioSource[] listeAudioSource;
    private int indexMusic = 0;

    private void Update()
    {
        if(listeAudioSource[0].isPlaying)
            DeplacementCamera.instance.JouerAnimationTramblement();

        if (Input.GetKeyDown(KeyCode.F2))
            PasserMusic();
    }

    public void JouerSonHorde()
    {
        listeAudioSource[0].PlayOneShot(listeSonFloodHord[Random.Range(0, listeSonFloodHord.Length)]); 
    }

    public void JouerMusic()
    {
        if(!listeAudioSource[1].isPlaying && !listeAudioSource[0].isPlaying)
        {
            indexMusic = Random.Range(0, listeMusic.Length);
            listeAudioSource[1].PlayOneShot(listeMusic[indexMusic]);
        }
    }

    public void StopperMusic()
    {
        listeAudioSource[1].Stop();
    }

    private void PasserMusic()
    {
        indexMusic = (indexMusic + 1) % listeMusic.Length;

        listeAudioSource[1].Stop();
        listeAudioSource[1].PlayOneShot(listeMusic[indexMusic]);
    }
}
