using System;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    public event Action OnHealthChanged;
    public event Action Dead;
    public int MaxHealth => _maxHealth;
    public int Health => _health;

    [SerializeField] private PlayerData _playerData;

    private int _maxHealth;
    private int _health;
    private int _armor;

    private void Start()
    {
        _maxHealth = _playerData.MaxHealth;
        _armor = _playerData.Armor;

        HealOnMaxValue();
    }

    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException($"damage could be more than 0, current: {damage}");

        _health = Mathf.Clamp(_health - (damage - _armor), 0, _maxHealth);

        OnHealthChanged?.Invoke();

        if (_health == 0)
            Dead?.Invoke();
    }

    public void HealOnMaxValue()
    {
        _health = _maxHealth;
        OnHealthChanged?.Invoke();
    }
}