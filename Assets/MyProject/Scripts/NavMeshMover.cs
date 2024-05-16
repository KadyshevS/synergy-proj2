using UnityEngine;
using UnityEngine.AI;

public class NavMeshMover : MonoBehaviour
{
    [SerializeField] private NavMeshAgent  _agent; 
    [SerializeField] private Animator      _animator;
    
    public float RemainingDistance => _agent.remainingDistance;

    public void MoveTo(Vector3 position, float velocity = 1f)
    {
        _agent.isStopped = false;
        _agent.SetDestination(position);
        _agent.speed = 3.5f * velocity;
        _animator.SetFloat("Vertical Speed", _agent.velocity.magnitude * velocity);
    }
    public void Stop()
    {
        _agent.isStopped = true;
        _animator.SetFloat("Vertical Speed", 0.0f);
    }
}
