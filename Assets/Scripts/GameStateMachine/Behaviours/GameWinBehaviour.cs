using UnityEngine;

public class GameWinBehaviour : BehaviourState
{
    private Canvas _winUI;

    public GameWinBehaviour(Canvas winUI)
    {
        _winUI = winUI;
    }

    public override void Enter()
    {
        _winUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _winUI.gameObject.SetActive(false);
    }
}
