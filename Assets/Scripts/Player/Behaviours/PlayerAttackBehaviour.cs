using UnityEngine;

public class PlayerAttackBehaviour : BehaviourState
{
    private PlayerFightStateMachine _playerFightStateMachine;
    public PlayerAttackBehaviour(PlayerFightStateMachine playerFightStateMachine)
    {
        _playerFightStateMachine = playerFightStateMachine;
    }

    public override void Update()
    {
        _playerFightStateMachine.UpdatePhase();
    }

    public override void Exit()
    {
        _playerFightStateMachine.ResetPhases();
    }
}
