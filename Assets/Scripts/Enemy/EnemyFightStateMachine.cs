using System;

public class EnemyFightStateMachine : EntityFightStateMachine
{
    public event Action Attacked;

    private EnemyData _enemyData;

    private EntityAttackFightPhase _attackPhase;
    private EntityCooldownFightPhase _cooldownPhase;

    private IDamagable _playerHealth;

    protected override void InitBehaviours()
    {
        _attackPhase = new EntityAttackFightPhase(_enemyData.AttackTime);
        _cooldownPhase = new EntityCooldownFightPhase(_enemyData.CooldownTime);

        _attackPhase.SetNextPhase(_cooldownPhase);
        _cooldownPhase.SetNextPhase(_attackPhase);
    }

    protected override void SetStartPhase()
    {
        _currentPhase = _attackPhase;
    }

    private void OnAttackPhaseDone()
    {
        Attacked?.Invoke();
        _playerHealth.TakeDamage(_enemyData.Damage);
    }

    protected override void Subscribe()
    {
        _attackPhase.Done += OnAttackPhaseDone;
    }

    protected override void Unsubscribe()
    {
        _attackPhase.Done -= OnAttackPhaseDone;
    }

    public void InitializeEnemyData(EnemyData enemyData)
    {
        _enemyData = enemyData;
        InitBehaviours();
        SetStartPhase();
        Subscribe();
    }

    public void SetPlayerHealth(IDamagable playerHealth)
    {
        _playerHealth = playerHealth;
    }
}