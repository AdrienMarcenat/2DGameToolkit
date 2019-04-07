public enum EGameFlowAction
{
    Resume,
    Retry,
    Start,
    Quit,
}

public class GameFlowEvent : GameEvent
{
    public GameFlowEvent (EGameFlowAction action) : base ("Game")
    {
        m_Action = action;
    }

    public EGameFlowAction GetAction ()
    {
        return m_Action;
    }

    private EGameFlowAction m_Action;
}

public class GameFlowHSM : HSM
{
    public GameFlowHSM ()
        : base (new GameFlowMenuState ()
              , new GameFlowNormalState ()
              , new GameFlowPauseState ()
              , new GameFlowGameOverState ()
        )
    {
    }
    public void StartFlow()
    {
        Start(typeof(GameFlowMenuState));
        this.RegisterToUpdate(false, EUpdatePass.Last);
    }

    public void StopFlow()
    {
        this.UnregisterToUpdate(EUpdatePass.Last);
        Stop();
    }
}