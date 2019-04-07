using UnityEngine;

public class World : MonoBehaviour
{
    private UnityLogger m_Logger;
    private Updater m_Updater;
    private GameEventManager m_GameEventManager;
    private InputManager m_InputManager;
    private LevelManager m_LevelManager;

    private GameFlowHSM m_GameFlowHSM;

    private static World ms_Instance;

    // This should be called before any other gameobject awakes
    private void Awake ()
    {
        // Singleton pattern : this is the only case where it should be used
        if(ms_Instance == null)
        {
            ms_Instance = this;
            DontDestroyOnLoad (gameObject);

            m_Logger = new UnityLogger();
            m_Updater = new Updater();
            m_GameEventManager = new GameEventManager();
            m_InputManager = new InputManager();
            m_LevelManager = new LevelManager();
            m_GameFlowHSM = new GameFlowHSM();
            OpenProxies();
            OnEngineStart();
        }
        else if (ms_Instance != this)
        {
            Destroy (gameObject);
            return;
        }
    }

    private void Shutdown()
    {
        if (ms_Instance == this)
        {
            this.DebugLog("Shutdown");
            OnEngineStop();
            CloseProxies();
        }
    }

    void OpenProxies()
    {
        LoggerProxy.Open(m_Logger);
        UpdaterProxy.Open(m_Updater);
        GameEventManagerProxy.Open(m_GameEventManager);
        InputManagerProxy.Open(m_InputManager);
        LevelManagerProxy.Open(m_LevelManager);
    }

    void CloseProxies()
    {
        LoggerProxy.Close(m_Logger);
        UpdaterProxy.Close(m_Updater);
        GameEventManagerProxy.Close(m_GameEventManager);
        InputManagerProxy.Close(m_InputManager);
        LevelManagerProxy.Close(m_LevelManager);
    }

    void OnEngineStart()
    {
        m_GameFlowHSM.StartFlow();
        m_InputManager.OnEngineStart();
        m_GameEventManager.OnEngineStart();
    }

    void OnEngineStop()
    {
        m_GameEventManager.OnEngineStop();
        m_InputManager.OnEngineStop();
        m_GameFlowHSM.StopFlow();
    }

    void Update ()
    {
        m_Updater.Update ();
    }
}
