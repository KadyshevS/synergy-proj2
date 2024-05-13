using UnityEngine;

public class Attacker : MonoBehaviour
{
    public bool CanAttack => _attackTimer <= 0f;
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
    [SerializeField] private Vector3    _attackRangeOffset;
    [SerializeField] private float      _attackRange;

    private float      _attackTimer;
    private Collider[] _hits;

    private void ResetAttackTimer() => _attackTimer = _attackColldown;

    void Start()
    {
        ResetAttackTimer();
        _hits = new Collider[3];
    }
    void Update()
    {
        _attackTimer -= Time.deltaTime;
    }
    
    public void Attack()
    {
        _animator.SetInteger("AttackIndex", Random.Range(0, 2));
        _animator.SetTrigger("Attacking");
        ResetAttackTimer();
        AttackEnemies();
    }
    public void CancelAttack()
    {
        _animator.SetBool("Attacking", false);
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
