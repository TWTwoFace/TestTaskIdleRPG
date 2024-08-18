using System;
using UnityEngine;

[RequireComponent(typeof(PlayerWeaponSwitcher))]
public class PlayerFightStateMachine : EntityFightStateMachine
{
    public event Action Attacked;
    public event Action<WeaponData> WeaponSwitched;

    [SerializeField] private float _cooldownTime;
    [SerializeField] private float _switchWeaponTime;

    [SerializeField] private EnemySpawner _enemySpawner;

    private EntityAttackFightPhase _attackPhase;
    private EntityCooldownFightPhase _cooldownPhase;
    private EntitySwitchWeaponFightPhase _switchWeaponPhase;

    private PlayerWeaponSwitcher _weaponSwitcher;

    private IDamagable _enemyHealth;

    private void Awake()
    {
        _weaponSwitcher = GetComponent<PlayerWeaponSwitcher>();
    }

    protected override void InitBehaviours()
    {
        _attackPhase = new EntityAttackFightPhase(_weaponSwitcher.GetDefaultWeaponData().AttackTime);
        _cooldownPhase = new EntityCooldownFightPhase(_cooldownTime);
        _switchWeaponPhase = new EntitySwitchWeaponFightPhase(_switchWeaponTime);

        _attackPhase.SetNextPhase(_cooldownPhase);
        _cooldownPhase.SetNextPhase(_attackPhase);
        _switchWeaponPhase.SetNextPhase(_cooldownPhase);
    }

    protected override void SetStartPhase()
    {
        _currentPhase = _attackPhase;
    }

    private void OnSwitchWeaponPhaseDone()
    {
        _attackPhase.SetNextPhase(_cooldownPhase);
        _weaponSwitcher.OnWeaponSwitched();
        WeaponSwitched?.Invoke(_weaponSwitcher.CurrentWeapon);
    }

    private void SwitchWeapon(WeaponData newWeaponData)
    {
        _attackPhase.SetNewTimeToDoPhase(newWeaponData.AttackTime);

        if (_currentPhase == _attackPhase)
        {
            _currentPhase.SetNextPhase(_switchWeaponPhase);
            return;
        }
        SwitchPhase(_switchWeaponPhase);
    }

    private void SetEnemyHealth(EnemyHealth enemyHealth)
    {
        _enemyHealth = enemyHealth;
    }

    private void OnAttackPhaseDone()
    {
        Attacked?.Invoke();
        _enemyHealth.TakeDamage(_weaponSwitcher.CurrentWeapon.damage);
    }

    protected override void Subscribe()
    {
        _switchWeaponPhase.Done += OnSwitchWeaponPhaseDone;
        _weaponSwitcher.Switched += SwitchWeapon;
        _enemySpawner.Spawned += SetEnemyHealth;
        _attackPhase.Done += OnAttackPhaseDone;
    }

    protected override void Unsubscribe()
    {
        _switchWeaponPhase.Done -= OnSwitchWeaponPhaseDone;
        _weaponSwitcher.Switched -= SwitchWeapon;
        _enemySpawner.Spawned -= SetEnemyHealth;
        _attackPhase.Done -= OnAttackPhaseDone;
    }
}