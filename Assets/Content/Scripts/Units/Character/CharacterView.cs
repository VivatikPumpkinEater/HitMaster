using UnityEngine;
using UnityEngine.AI;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private NavMeshAgent _navMeshAgent;
    
    private readonly int Speed = Animator.StringToHash("Speed");

    public NavMeshAgent NavMeshAgent => _navMeshAgent;
    
    private void Update()
    {
        _animator.SetFloat(Speed, _navMeshAgent.velocity.magnitude);
    }
}