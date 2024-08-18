using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    protected Dictionary<Type, BehaviourState> _behavioursMap;
    protected BehaviourState _currentBehaviour;

    protected virtual void Awake()
    {
        InitBehaviours();
    }

    private void Start()
    {
        SetBehaviourByDefault();
    }

    private void Update()
    {
        _currentBehaviour.Update();
    }

    protected virtual void InitBehaviours()
    {
        _behavioursMap = new Dictionary<Type, BehaviourState>();
    }

    protected void SetBehaviour(BehaviourState newBehaviour)
    {
        if (newBehaviour == _currentBehaviour)
        {
            return;
        }
        _currentBehaviour?.Exit();
        _currentBehaviour = newBehaviour;
        _currentBehaviour?.Enter();
    }

    protected BehaviourState GetBehaviour<T>() where T : BehaviourState
    {
        var type = typeof(T);
        return _behavioursMap[type];
    }

    protected virtual void SetBehaviourByDefault()
    {
        return;
    }

    protected virtual void Subscribe()
    {
        return;
    }

    protected virtual void Unsubscribe()
    {
        return;
    }

    protected virtual void OnEnable()
    {
        Subscribe();
    }

    protected virtual void OnDisable()
    {
        Unsubscribe();
    }
}