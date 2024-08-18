using UnityEngine;

public class GameInFightBehaviour : BehaviourState
{
    private PlayerStateMachine _playerStateMachine;
    private EnemySpawner _enemySpawner;
    private Canvas _inFightUI;

    private EnemyFightStateMachine _enemyFightStateMachine;

    public GameInFightBehaviour(PlayerStateMachine playerStateMachine, EnemySpawner enemySpawner, Canvas inFightUI)
    {
        _playerStateMachine = playerStateMachine;
        _enemySpawner = enemySpawner;
        _inFightUI = inFightUI;
    }

    public override void Enter()
    {
        _playerStateMachine.SetAttackBehaviour();
        _enemyFightStateMachine = _enemySpawner.Spawn();
        _inFightUI.gameObject.SetActive(true);
        _enemyFightStateMachine.SetPlayerHealth(_playerStateMachine.GetComponent<PlayerHealth>());
    }

    public override void Update()
    {
        _enemyFightStateMachine.UpdatePhase();
    }

    public override void Exit()
    {
        _playerStateMachine.SetIdleBehaviour();
        _enemySpawner.Despawn();
        _inFightUI.gameObject.SetActive(false);
    }
}
