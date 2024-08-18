using System;

public interface IDamagable
{
    public event Action OnHealthChanged;
    public event Action Dead;

    public int Health { get; }
    public int MaxHealth { get; }

    public void TakeDamage(int damage);
}

