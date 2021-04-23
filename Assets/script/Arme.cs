using System.Collections;
using UnityEngine;

public class Arme : MonoBehaviour
{
    [SerializeField] private float cadenceTir;
    [SerializeField] private float vitesseBalle;
    [SerializeField] private float tempsRechargement;
    [SerializeField] [Range(0f, 0.1f)] private float dispersionTir = 0.05f;

    [SerializeField] private string tagCible;

    [SerializeField] private int balleChargeur;
    [SerializeField] private int degats;

    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform canon = null;
    [SerializeField] private ZoneDectection zoneDectection = null;
    [SerializeField] private GameObject personnage = null;

    [SerializeField] private AudioClip sonTir = null;
    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private ParticleSystem muzzleFlash = null;

    private bool recharge;
    private int tempoBalleChargeur;

    private void Start()
    {
        tempoBalleChargeur = balleChargeur;
    }

    public void Tirer()
    {
        if(!recharge)
        {
            audioSource.PlayOneShot(sonTir);
            muzzleFlash.Emit(1);

            GameObject _obj = Instantiate(projectile, canon.position, canon.rotation);
            _obj.GetComponent<Projectile>().Initialiser(transform.position, zoneDectection.GetRadius(), tagCible, degats, personnage, vitesseBalle, dispersionTir);
            balleChargeur--;

            if(balleChargeur == 0)
            {
                StartCoroutine(Recharger());
            }
        }
    }

    public float GetCadenceTir()
    {
        return cadenceTir;
    }

    private IEnumerator Recharger()
    {
        recharge = true;
        yield return new WaitForSeconds(tempsRechargement);
        recharge = false;

        balleChargeur = tempoBalleChargeur;
    }
}
