using UnityEngine;

public class GameLooseBehaviour : BehaviourState
{
    private Canvas _looseUI;

    public GameLooseBehaviour(Canvas looseUI)
    {
        _looseUI = looseUI;
    }

    public override void Enter()
    {
        _looseUI.gameObject.SetActive(true);
    }

    public override void Exit()
    {
        _looseUI.gameObject.SetActive(false);
    }
}
