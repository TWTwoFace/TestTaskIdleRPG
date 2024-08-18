using UnityEngine;

public class GameStateMachine : StateMachine
{
    [Header("Player:")]
    [SerializeField] private PlayerStateMachine _playerStateMachine;
    [SerializeField] private PlayerHealth _playerHealth;

    [Header("Enemy:")]
    [SerializeField] private EnemySpawner _enemySpawner;

    [Header("UI:")]
    [SerializeField] private Canvas _lobbyUI;
    [SerializeField] private Canvas _inFightUI;
    [SerializeField] private Canvas _looseUI;
    [SerializeField] private Canvas _winUI;


    protected override void InitBehaviours()
    {
        base.InitBehaviours();
        _behavioursMap[typeof(GameLobbyBehaviour)] = new GameLobbyBehaviour(_playerStateMachine, _lobbyUI);
        _behavioursMap[typeof(GameInFightBehaviour)] = new GameInFightBehaviour(_playerStateMachine, _enemySpawner, _inFightUI);
        _behavioursMap[typeof(GameLooseBehaviour)] = new GameLooseBehaviour(_looseUI);
        _behavioursMap[typeof(GameWinBehaviour)] = new GameWinBehaviour(_winUI);
    }

    protected override void SetBehaviourByDefault()
    {
        var behaviour = GetBehaviour<GameLobbyBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetInFightBehaviour()
    {
        var behaviour = GetBehaviour<GameInFightBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetLobbyBehaviour()
    {
        var behaviour = GetBehaviour<GameLobbyBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetLooseBehaviour()
    {
        var behaviour = GetBehaviour<GameLooseBehaviour>();
        SetBehaviour(behaviour);
    }

    public void SetWinBehaviour()
    {
        var behaviour = GetBehaviour<GameWinBehaviour>();
        SetBehaviour(behaviour);
    }

    protected override void Subscribe()
    {
        _playerHealth.Dead += SetLooseBehaviour;
        _enemySpawner.EnemyDead += SetWinBehaviour;
    }

    protected override void Unsubscribe()
    {
        _playerHealth.Dead -= SetLooseBehaviour;
        _enemySpawner.EnemyDead -= SetWinBehaviour;
    }
}
