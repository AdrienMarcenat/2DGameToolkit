using UnityEngine;
using UnityEditor;

public class GameFlowHSM : HSM
{
    public GameFlowHSM ()
        : base (new GameFlowNormalState ()
              , new GameFlowPauseState ()
        )
    {
        Start (typeof (GameFlowNormalState));
    }
}