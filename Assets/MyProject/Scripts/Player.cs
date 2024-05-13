using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputMover _mover;
    [SerializeField] private Attacker   _attacker;
    [SerializeField] private Health     _health;

    void FixedUpdate()
    {
        if (_health.Dead) return;

        _mover.Move();
    }
    void Update()
    {
        if (_health.Dead) return;

        if (Input.GetMouseButtonDown(0) && _attacker.CanAttack)
        {
            _attacker.Attack();
        }
        else
        {
            _attacker.CancelAttack();
        }
    }
}
