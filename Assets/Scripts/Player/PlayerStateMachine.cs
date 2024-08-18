using UnityEngine;

public class PlayerStateMachine : StateMachine
{
    private PlayerFightStateMachine _playerFightStateMachine;
    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(PlayerIdleBehaviour)] = new PlayerIdleBehaviour();
        _behavioursMap[typeof(PlayerAttackBehaviour)] = new PlayerAttackBehaviour(_playerFightStateMachine);
    }
    protected override void Awake()
    {
        _playerFightStateMachine = GetComponent<PlayerFightStateMachine>();
        base.Awake();
    }

    public void SetAttackBehaviour()
    {
        var behaviour = GetBehaviour<PlayerAttackBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetIdleBehaviour()
    {
        var behaviour = GetBehaviour<PlayerIdleBehaviour>();
        SetBehaviour(behaviour);
    }


    protected override void SetBehaviourByDefault()
    {
        var behaviour = GetBehaviour<PlayerIdleBehaviour>();
        SetBehaviour(behaviour);
    }
}
