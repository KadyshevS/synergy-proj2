using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        None = 0,
        Idle, Patrol, Follow, Attacking, Dead
    }

    [SerializeField] private Attacker       _attacker;
    [SerializeField] private NavMeshMover   _mover;
    [SerializeField] private Health         _health;
    [SerializeField] private float          _distanceToFollow;
    [SerializeField] private float          _distanceToAttack;
    [SerializeField] private List<Vector3>  _patrolPoints;
    [SerializeField] private float          _idleBetweenPoints;
    private Player    _player;
    private State     _currentState;
    private Vector3   _currentPatrolPoint;
    private float     _idleTimer;

    private void ResetPatrolPoint() => _currentPatrolPoint = _patrolPoints[Random.Range(0, _patrolPoints.Count)];
    private void ResetIdleTimer() => _idleTimer = _idleBetweenPoints;  
    private void SetState(State state)
    {
        if (_currentState != state)
            _currentState = state;
    }

    void Start()
    {
        _player = FindObjectOfType<Player>();
        _currentState = State.Idle;
        ResetIdleTimer();
        ResetPatrolPoint();
    }
    void FixedUpdate()
    {
        if (_health.Dead)
        {
            SetState(State.Dead);
            return;
        }
        
        if (
            Vector3.Distance(transform.position, _player.transform.position) <= _distanceToAttack && 
            !_player.GetComponent<Health>().Dead &&
            _attacker.CanAttack)
        {
            _attacker.Attack();
            _mover.Stop();
            SetState(State.Attacking);
        }
        else if (
            Vector3.Distance(transform.position, _player.transform.position) <= _distanceToFollow && 
            !_player.GetComponent<Health>().Dead &&
            !_attacker.IsAttacking)
        {
            _mover.MoveTo(_player.transform.position);
            SetState(State.Follow);
        }
        else
        {
            if (_idleTimer > 0f || _attacker.IsAttacking) return;

            _mover.MoveTo(_currentPatrolPoint, 0.7f);
            SetState(State.Patrol);
            
            if (_mover.RemainingDistance < 0.1f)
            {
                Debug.Log("Reset");
                ResetPatrolPoint();
                ResetIdleTimer();
                SetState(State.Idle);
            }
        }
    }
    void Update()
    {
        if (_currentState == State.Idle)
            _idleTimer -= Time.deltaTime;
    }
    void OnDrawGizmosSelected()
    {
        foreach (var p in _patrolPoints)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(p, 1f);
        }
    }
}
