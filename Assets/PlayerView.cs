using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    [Header("Weapons:")]
    [SerializeField] private SpriteRenderer _swordSprite;
    [SerializeField] private SpriteRenderer _bowSprite;
    [SerializeField] private WeaponData _swordData;
    [SerializeField] private WeaponData _bowData;


    private PlayerFightStateMachine _playerFightStateMachine;
    private PlayerHealth _playerHealth;

    private void Awake()
    {
        _playerFightStateMachine = GetComponent<PlayerFightStateMachine>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void OnAttack()
    {
        _animator.Play("Attack");
    }

    private void OnDead()
    {
        _animator.Play("Die");
    }

    private void OnWeaponSwitched(WeaponData newWeapon)
    {
        bool value = newWeapon == _swordData;
        _swordSprite.enabled = value;
        _bowSprite.enabled = !value;
    }

    private void OnEnable()
    {
        _playerFightStateMachine.Attacked += OnAttack;
        _playerHealth.Dead += OnDead;
        _playerFightStateMachine.WeaponSwitched += OnWeaponSwitched;
    }

    private void OnDisable()
    {
        _playerFightStateMachine.Attacked -= OnAttack;
        _playerHealth.Dead -= OnDead;
        _playerFightStateMachine.WeaponSwitched -= OnWeaponSwitched;
    }
}
