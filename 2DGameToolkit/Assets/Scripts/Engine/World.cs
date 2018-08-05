using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
    private Updater m_Updater;
    private GameEventManager m_GameEventManager;
    private InputManager m_InputManager;

    private GameFlowHSM m_GameFlowHSM;

    // This should be called before any other gameobject awake
    private void Awake ()
    {
        DontDestroyOnLoad (gameObject);
        // Keep the Updater first, as the other members might want to register to it
        m_Updater = new Updater ();
        UpdaterProxy.Open (m_Updater);
        m_GameEventManager = new GameEventManager ();
        GameEventManagerProxy.Open (m_GameEventManager);
        m_InputManager = new InputManager ();
        InputManagerProxy.Open (m_InputManager);

        m_GameFlowHSM = new GameFlowHSM ();
    }

    void Update ()
    {
        m_Updater.Update ();
    }

    private void OnDestroy ()
    { 
        InputManagerProxy.Close();
        GameEventManagerProxy.Close ();
        UpdaterProxy.Close ();
    }
}
