using UnityEngine;
using UnityEditor;

public class GameFlowNormalState : HSMState
{
    public override void OnEnter ()
    {
        this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
    }

    public void OnGameEvent (PlayerInputGameEvent inputGameEvent)
    {
        if (inputGameEvent.GetInput () == "space" && !UpdaterProxy.Get().IsPaused())
        {
            ChangeNextTransition (HSMTransition.EType.Child, typeof(GameFlowPauseState));
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Player");
    }
}