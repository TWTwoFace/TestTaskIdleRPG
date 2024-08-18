
using UnityEngine;

public class GameLobbyBehaviour : BehaviourState
{
    private PlayerStateMachine _playerStateMachine;
    private Canvas _lobbyUI;

    public GameLobbyBehaviour(PlayerStateMachine playerStateMachine, Canvas lobbyUI)
    {
        _playerStateMachine = playerStateMachine;
        _lobbyUI = lobbyUI;
    }

    public override void Enter()
    {
        _playerStateMachine.SetIdleBehaviour();
        _lobbyUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _lobbyUI.gameObject.SetActive(false);
    }
}
