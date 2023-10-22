using UnityEngine;
using UnityEngine.AI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    
    private readonly int Speed = Animator.StringToHash("Speed");

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    public Vector3 ShootPointPosition => _shootPoint.position;
    public NavMeshAgent NavMeshAgent => _navMeshAgent ??= GetComponent<NavMeshAgent>();
    private Animator Animator => _animator ??= GetComponent<Animator>();
    
    private void Update()
    {
        Animator.SetFloat(Speed, NavMeshAgent.velocity.magnitude);
    }
}