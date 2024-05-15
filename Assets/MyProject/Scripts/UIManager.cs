using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Health          _health;
    [SerializeField] private Slider          _slider;
    [SerializeField] private TextMeshProUGUI _text;

    void Start()
    {
        UpdateMaxHealth();
        UpdateCurHealth();
    }
    void Update()
    {
        if (_health.CurrentHealth != _slider.value)
            UpdateCurHealth();
    }

    public void UpdateMaxHealth() => _slider.maxValue = _health.MaxHealth;
    public void UpdateCurHealth()
    {
        _slider.value = _health.CurrentHealth;
        _text.text = System.Convert.ToString(_slider.value);
    }
}
