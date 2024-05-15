using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Attacker     _attacker;
    [SerializeField] private NavMeshMover _mover;
    [SerializeField] private Health       _health;
    [SerializeField] private float        _minDistanceToPlayer;

    private Transform _player;

    void Start()
    {
        _player = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        if (_health.Dead) return;

        if (Vector3.Distance(transform.position, _player.position) <= _minDistanceToPlayer)
        {
            if (_attacker.CanAttack)
                _attacker.Attack();
            _mover.Stop();
        }
        else
        {
            _attacker.CancelAttack();
            _mover.MoveTo(_player.position);
        }
    }
}
