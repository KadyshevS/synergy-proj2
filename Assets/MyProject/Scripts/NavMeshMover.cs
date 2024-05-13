using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent  _agent; 
    [SerializeField] private Animator      _animator;

    public void MoveTo(Vector3 position)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
        _animator.SetFloat("Vertical Speed", _agent.velocity.magnitude);
    }
    public void Stop()
    {
        _agent.isStopped = true;
        _animator.SetFloat("Vertical Speed", 0.0f);
    }
}
