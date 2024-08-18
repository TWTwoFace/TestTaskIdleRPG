using System;
using UnityEngine;

public class PlayerWeaponSwitcher : MonoBehaviour
{
    public event Action<WeaponData> Switched;

    public WeaponData CurrentWeapon => _currentWeapon;

    [SerializeField] private WeaponData _swordData;
    [SerializeField] private WeaponData _bowData;

    private WeaponData _currentWeapon;

    private bool _canSwitch = true;

    private void Awake()
    {
        SetDefaultWeaponData();
    }
    
    private void SetDefaultWeaponData()
    {
        _currentWeapon = _swordData;
    }

    public WeaponData GetDefaultWeaponData()
    {
        return _swordData;
    }

    public void OnWeaponSwitched()
    {
        _canSwitch = true;
    }

    public void SwitchWeapon()
    {
        if (_canSwitch == false)
            return;

        if (_currentWeapon == _swordData)
            _currentWeapon = _bowData;
        else
            _currentWeapon = _swordData;

        _canSwitch = false;

        Switched?.Invoke(_currentWeapon);
    }


}
