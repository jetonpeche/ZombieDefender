using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class Arme : MonoBehaviour
{
    [SerializeField] private bool estDeuxiemeArme;

    [SerializeField] private float cadenceTir;
    [SerializeField] private float vitesseBalle;
    [SerializeField] private float tempsRechargement;
    [SerializeField] [Range(0f, 0.1f)] private float dispersionTir = 0.05f;

    [SerializeField] private string tagCible;
    [SerializeField] private string nomTrigger;

    [SerializeField] private int balleChargeur;
    [SerializeField] private int degats;

    [SerializeField] private GameObject projectile = null;
    [SerializeField] private Transform canon = null;
    [SerializeField] private ZoneDectection zoneDectection = null;
    [SerializeField] private GameObject personnage = null;
    [SerializeField] private AnimatorEvent animatorEvent = null;
    [SerializeField] private Animator animator = null;

    [SerializeField] private AudioClip sonTir = null;
    [SerializeField] private AudioSource audioSource = null;

    [SerializeField] private ParticleSystem muzzleFlash = null;

    [SerializeField] private RigTransform rigTransform = null;
    [SerializeField] private Rigidbody rb = null;
    [SerializeField] private Collider col = null;

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
            if(audioSource != null)
                audioSource.PlayOneShot(sonTir);

            muzzleFlash.Emit(1);

            GameObject _obj = Instantiate(projectile, canon.position, canon.rotation);
            _obj.GetComponent<Projectile>().Initialiser(transform.position, zoneDectection.GetRadius(), tagCible, degats, personnage, vitesseBalle, dispersionTir);
            balleChargeur--;

            if(balleChargeur == 0)
            {
                recharge = true;

                if(animator != null)
                    animator.SetTrigger(nomTrigger);
                else
                {
                    StartCoroutine(RechargerCoroutine());
                }
            }
        }
    }

    public float GetCadenceTir()
    {
        return cadenceTir;
    }

    public GameObject GetPerso()
    {
        return personnage;
    }

    public void Recharger()
    {
        balleChargeur = tempoBalleChargeur;
        recharge = false;
    }

    public void DetacherArme()
    {
        rb.useGravity = true;
        col.enabled = true;
        zoneDectection.BouttonActiverCibleAuto(false);

        if(!estDeuxiemeArme)
            zoneDectection.StopAttaque();

        Destroy(rigTransform);
    }

    private IEnumerator RechargerCoroutine()
    {
        yield return new WaitForSeconds(tempsRechargement);
        Recharger();
    }
}
