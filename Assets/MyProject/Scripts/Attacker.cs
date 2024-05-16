using UnityEngine;

public class Attacker : MonoBehaviour
{
    public bool CanAttack => _attackTimer <= 0f;
    public bool IsAttacking => _attackingTimer > 0f;
    public bool MaskInRange =>
        Physics.CheckSphere(
            transform.position + 
            transform.forward.normalized * _attackRangeOffset.z +
            transform.up.normalized * _attackRangeOffset.y +
            transform.right.normalized * _attackRangeOffset.x, 
            _attackRange, _attackMask);

    [SerializeField] private Animator   _animator; 
    [SerializeField] private LayerMask  _attackMask;
    [SerializeField] private float      _damage;
    [SerializeField] private float      _attackColldown;
    [SerializeField] private float      _attackDuration;
    [SerializeField] private Vector3    _attackRangeOffset;
    [SerializeField] private float      _attackRange;

    private int        _attackIndex;
    private float      _attackTimer;
    private float      _attackingTimer;
    private Collider[] _hits;

    private void ResetAttackTimer() => _attackTimer = _attackColldown;
    private void ResetAttackingTimer() => _attackingTimer = _attackDuration * (_attackIndex + 1f);

    void Start()
    {
        ResetAttackTimer();
        _attackTimer = -1f;
        _hits = new Collider[3];
    }
    void Update()
    {
        _attackTimer -= Time.deltaTime;
        _attackingTimer -= Time.deltaTime;

        if (_attackingTimer > 0f)
            _animator.SetTrigger("Attacking");
        else
            _animator.SetBool("Attacking", false);
    }
    
    public void Attack()
    {
        if (_attackingTimer <= 0f)
        {
            _attackIndex = Random.Range(0, 2);
            _animator.SetInteger("AttackIndex", _attackIndex);
            ResetAttackTimer();
            ResetAttackingTimer();
            AttackEnemies();
        }
    }

    void AttackEnemies()
    {
        Physics.OverlapSphereNonAlloc(
            transform.position + 
            transform.forward.normalized * _attackRangeOffset.z +
            transform.up.normalized * _attackRangeOffset.y +
            transform.right.normalized * _attackRangeOffset.x, 
            _attackRange, _hits, _attackMask);

        for (int i = 0; i < _hits.Length; i++)
        {
            if (!_hits[i]) break;
            if (_hits[i].TryGetComponent(out Health health))
            {
                health.TakeDamage(_damage);
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(
            transform.position + 
            transform.forward.normalized * _attackRangeOffset.z +
            transform.up.normalized * _attackRangeOffset.y +
            transform.right.normalized * _attackRangeOffset.x, 
            _attackRange);
    }
}
