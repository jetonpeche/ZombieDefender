using UnityEngine;

public class FonduNoir : MonoBehaviour
{
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Transform ptSpawn = null;
    [SerializeField] private float interval;
    [SerializeField] private int multiplicateurScale = 5;

    [SerializeField] AudioSource audioSource = null;
    [SerializeField] private Animator animator = null;

    private bool estJouer;
    private float intervalTempo;

    // 64 est le mini et le nb doit etre un multiple de 2
    private float[] echantillonSon = new float[64];

    // 33 = nb de prefab a instancier
    private GameObject[] echantillonBar = new GameObject[33];

    private void Start()
    {
        intervalTempo = interval;

        for (int i = 0; i < echantillonBar.Length; i++)
        {
            GameObject _obj = Instantiate(prefab);

            _obj.transform.parent = transform;
            // _obj.name = "cube " + i;
            _obj.transform.position = ptSpawn.position + new Vector3(intervalTempo, 0, 0);
            intervalTempo += interval;
            echantillonBar[i] = _obj;
        }
    }

    void Update()
    {
        if(!audioSource.isPlaying)
        {
            if (!estJouer)
            {
                estJouer = true;
                animator.SetTrigger("jouer");
            }
        }
        else
        {
            // recupere les echantillons de l'audio et les convertie en signaux triangulaire. 
            audioSource.GetSpectrumData(echantillonSon, 0, FFTWindow.Triangle);

            for (int i = 0; i < echantillonBar.Length; i++)
            {
                echantillonBar[i].transform.localScale = new Vector3(1, (echantillonSon[i] * multiplicateurScale) + 0.05f, 1);
            }
        }
    }

    public void DetruireObj()
    {
        Destroy(gameObject);
    }
}
