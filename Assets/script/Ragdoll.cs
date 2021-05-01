using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Animator[] listeAnimator;
    [SerializeField] private Collider hitBox;

    // liste des elements du ragDoll
    private Collider[] listCollider;
    private Rigidbody[] listRb;

    private void Awake()
    {
        listCollider = GetComponentsInChildren<Collider>();
        listRb = GetComponentsInChildren<Rigidbody>();

        ActiverRagDoll(false);
    }

    public void ActiverRagDoll(bool _stat)
    {
        foreach (Animator _anim in listeAnimator)
        {
            _anim.enabled = !_stat;
        }

        foreach (Collider _col in listCollider)
        {
            _col.enabled = _stat;
        }

        foreach (Rigidbody _rb in listRb)
        {
            _rb.isKinematic = !_stat;
        }

        hitBox.enabled = !_stat;
    }
}
