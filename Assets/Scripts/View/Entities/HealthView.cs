using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(IDamagable))]
public class HealthView : MonoBehaviour
{
    [SerializeField] private Image _fillerHealthBar;
    [SerializeField] private TMP_Text _healthAmountText;

    private IDamagable _damagable;

    private void Awake()
    {
        _damagable = GetComponent<IDamagable>();
    }

    private void Start()
    {
        SetHealthAmountText();
    }

    private void SetImageFillAmount()
    {
        _fillerHealthBar.fillAmount = _damagable.Health * 1f / _damagable.MaxHealth;
    }

    private void SetHealthAmountText()
    {
        _healthAmountText.text = $"{_damagable.Health}/{_damagable.MaxHealth}";
    }

    private void OnEnable()
    {
        _damagable.OnHealthChanged += SetImageFillAmount;
        _damagable.OnHealthChanged += SetHealthAmountText;
    }

    private void OnDisable()
    {
        _damagable.OnHealthChanged -= SetImageFillAmount;
        _damagable.OnHealthChanged -= SetHealthAmountText;
    }
}
