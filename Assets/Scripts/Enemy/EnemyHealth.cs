using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public event Action OnHealthChanged;
    public event Action Dead;

    public int MaxHealth => _maxHealth;
    public int Health => _health;

    private EnemyData _enemyData;

    private int _maxHealth;
    private int _health;


    public void TakeDamage(int damage)
    {
        if (damage <= 0)
            throw new ArgumentOutOfRangeException($"damage could be more than 0, current: {damage}");

        _health = Mathf.Clamp(_health - damage, 0, _maxHealth);

        OnHealthChanged?.Invoke();

        if (_health == 0)
            Dead?.Invoke();
    }

    public void SetEnemyData(EnemyData enemyData)
    {
        _enemyData = enemyData;
        _maxHealth = _enemyData.MaxHealth;
        _health = _maxHealth;
    }
}
