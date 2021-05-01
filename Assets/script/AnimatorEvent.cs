using UnityEngine;

public class AnimatorEvent : MonoBehaviour
{
    [SerializeField] private Arme arme;

    public void FinAnimation()
    {
        arme.Recharger();
    }
}
