using UnityEngine;
using UnityEditor;

public class GameFlowPauseState : HSMState
{
    public override void OnEnter ()
    {
        UpdaterProxy.Get ().SetPause (true);
        this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
    }

    public void OnGameEvent (PlayerInputGameEvent inpueGameEvent)
    {
        if (inpueGameEvent.GetInput () == "space")
        {
            ChangeNextTransition (HSMTransition.EType.Exit);
        }
    }

    public override void OnExit ()
    {
        this.UnregisterAsListener ("Player");
        UpdaterProxy.Get ().SetPause (false);
    }
}