using UnityEngine;

public class MortVehicule : MonoBehaviour
{
    [SerializeField] private ParticleSystem explosion = null;

    public void Mort()
    {
        explosion.Play();
    }
}
