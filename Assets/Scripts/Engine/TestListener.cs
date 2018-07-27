using UnityEngine;
using System.Collections;

public class TestListener : MonoBehaviourListener
{
    void Start ()
    {
        CreateListener ("Player", typeof(PlayerEvent), typeof(PlayerHealthEvent));
        RegisterAsListener ();
    }

    public void OnEvent(PlayerEvent playerEvent)
    {
        Debug.Log (playerEvent.GetMessage ());
    }

    public void OnEvent (PlayerHealthEvent playerHealthEvent)
    {
        Debug.Log (playerHealthEvent.GetMessage ());
    }
}
