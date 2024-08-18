using UnityEngine;
using UnityEngine.UI;

public class PhaseProgressView : MonoBehaviour
{
    [SerializeField] private Image _phaseProgressImage;
    [SerializeField] private EntityFightStateMachine _entityFightStateMachine;

    private void FixedUpdate()
    {
        _phaseProgressImage.fillAmount = _entityFightStateMachine.CurrentPhaseProgress;
    }
}
