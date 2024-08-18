using UnityEngine;
using System;

public abstract class EntityFightStateMachine : MonoBehaviour
{
    public float CurrentPhaseProgress => _currentPhase.PhaseProgress;

    protected EntityFightPhase _currentPhase;

    public void ResetPhases()
    {
        InitBehaviours();
        Subscribe();
        SetStartPhase();
    }

    private void Start()
    {
        ResetPhases();

        if (_currentPhase == null)
            throw new NullReferenceException("Realize Set Start Phase method");

        _currentPhase.Done += SwitchPhaseToNext;
    }

    protected abstract void SetStartPhase();

    protected abstract void InitBehaviours();

    public void UpdatePhase()
    {
        _currentPhase?.UpdatePhase();
    }

    protected void SwitchPhase(EntityFightPhase phase)
    {
        _currentPhase.Done -= SwitchPhaseToNext;
        _currentPhase = phase;
        _currentPhase.Done += SwitchPhaseToNext;
    }

    protected void SwitchPhaseToNext()
    {
        _currentPhase.Done -= SwitchPhaseToNext;
        _currentPhase = _currentPhase.NextPhase;
        _currentPhase.Done += SwitchPhaseToNext;
    }

    protected virtual void Subscribe() { }

    protected virtual void Unsubscribe() { }

    private void OnDestroy()
    {
        Unsubscribe();
    }
}