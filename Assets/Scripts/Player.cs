using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _maxHealth;

    private int _health;

    public event UnityAction<int, int> HealthChanged;

    private void Start()
    {
        _health = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        _health = Mathf.Max(_health - damage, 0);
        HealthChanged?.Invoke(_health, _maxHealth);
    }

    public void Heal(int power)
    {
        _health = Mathf.Min(_health + power, _maxHealth);
        HealthChanged?.Invoke(_health, _maxHealth);
    }
}
