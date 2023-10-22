using UnityEngine;

public class RagdollActivator : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _rigidbodies;
    
    private Animator _animator;
    private Collider _collider;
    private Animator Animator => _animator ??= GetComponent<Animator>();
    private Collider Collider => _collider ??= GetComponent<Collider>();

    public void Activate()
    {
        foreach (var rb in _rigidbodies)
            rb.isKinematic = false;

        Animator.enabled = false;
        Collider.enabled = false;
    }

#if UNITY_EDITOR

    [ContextMenu("Find All Rigidbodies")]
    public void FindAllRigidbodies()
    {
        _rigidbodies = GetComponentsInChildren<Rigidbody>();
    }
    
#endif
}