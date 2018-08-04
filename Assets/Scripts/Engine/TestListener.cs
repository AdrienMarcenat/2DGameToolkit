using UnityEngine;
using System.Collections;

public class TestListener : MonoBehaviour
{
    void Start ()
    {
        GameEventManagerProxy.Get().Register (this, "Player", typeof(PlayerInputGameEvent));
    }

    public void OnGameEvent (PlayerInputGameEvent inputGameEvent)
    {
        Debug.Log (inputGameEvent.GetInput ());
    }
}
