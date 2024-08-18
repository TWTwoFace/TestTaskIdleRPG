using System;
using UnityEngine;

public abstract class EntityFightPhase
{
    public event Action Done;
    public float PhaseProgress => _phaseTimer / _timeToDoPhase;
    public EntityFightPhase NextPhase => _nextPhase;

    private EntityFightPhase _nextPhase;

    private float _timeToDoPhase;
    private float _phaseTimer = 0;

    public EntityFightPhase(float timeToAction)
    {
        _timeToDoPhase = timeToAction;
    }

    public void SetNewTimeToDoPhase(float newTime)
    {
        if (newTime <= 0)
            throw new ArgumentOutOfRangeException("New time could be more than zero (newTime > 0)");

        _timeToDoPhase = newTime;
    }

    public void UpdatePhase()
    {
        if (_phaseTimer >= _timeToDoPhase)
        {
            _phaseTimer = 0;
            Done?.Invoke();
            return;
        }
        _phaseTimer += Time.deltaTime;
    }

    public void SetNextPhase(EntityFightPhase newNextPhase)
    {
        _nextPhase = newNextPhase;
    }

}