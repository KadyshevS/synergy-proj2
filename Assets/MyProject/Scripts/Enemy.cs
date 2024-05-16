using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        None = 0,
        Idle, Follow, Attacking, Dead
    }

    [SerializeField] private Attacker     _attacker;
    [SerializeField] private NavMeshMover _mover;
    [SerializeField] private Health       _health;
    [SerializeField] private float        _distanceToFollow;
    [SerializeField] private float        _distanceToAttack;
    private Transform _player;
    private State     _currentState;

    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
        _currentState = State.Idle;
    }
    void FixedUpdate()
    {
        if (_health.Dead)
        {
            if (_currentState != State.Dead)
                _currentState = State.Dead;
            return;
        }
        
        if (Vector3.Distance(transform.position, _player.position) <= _distanceToAttack && _attacker.CanAttack)
        {
            _attacker.Attack();
            _mover.Stop();
            _currentState = State.Attacking;
        }
        else if (Vector3.Distance(transform.position, _player.position) <= _distanceToFollow && !_attacker.IsAttacking)
        {
            _mover.MoveTo(_player.position);
            _currentState = State.Follow;
        }
        else
        {
            _mover.Stop();
            _currentState = State.Idle;
        }
    }
}
