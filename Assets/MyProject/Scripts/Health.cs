using UnityEngine;

public class Health : MonoBehaviour
{
    public bool Dead => _currentHealth <= 0f;
    public float MaxHealth => _maxHealth;
    public float CurrentHealth => _currentHealth;

    [SerializeField] private float _maxHealth;
    [SerializeField] private Animator _animator;
    private float _currentHealth;

    void Start()
    {
        _currentHealth = _maxHealth;
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
