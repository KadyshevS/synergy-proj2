using UnityEngine;

public class Health : MonoBehaviour
{
    public bool Dead => _currentHealth <= 0f;

    [SerializeField] private Animator _animator;
    [SerializeField] private float _startHealth;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _startHealth;
    }

    public void TakeDamage(float damage) 
    {
        _currentHealth -= damage;

        if (_currentHealth <= 0f)
            Die();
    }
    void Die()
    {
        _animator.SetTrigger("Dead");
    }
}
