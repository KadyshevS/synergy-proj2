using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Attacker     _attacker;
    [SerializeField] private NavMeshMover _mover;
    [SerializeField] private Health       _health;

    private Transform _playerTransform;

    void Start()
    {
        _playerTransform = FindObjectOfType<Player>().transform;
    }

    void FixedUpdate()
    {
        if (_health.Dead) return;
        Debug.Log(_attacker.MaskInRange);

        if (_attacker.MaskInRange)
        {
            if (_attacker.CanAttack)
                _attacker.Attack();
            _mover.Stop();
        }
        else
        {
            _attacker.CancelAttack();
            _mover.MoveTo(_playerTransform.position);
        }
    }
}
