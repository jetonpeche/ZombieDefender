using UnityEngine;

public class MortVehicule : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip sonMort = null;

    public void Mort()
    {
        audioSource.PlayOneShot(sonMort);
        explosion.Play();
    }
}
