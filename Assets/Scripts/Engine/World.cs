using UnityEngine;
using System.Collections;

public class World : MonoBehaviour
{
    private Updater m_Updater;
    private EventManager m_EventManager;

    // This should be called before any other gameobject awake
    private void Awake ()
    {
        DontDestroyOnLoad (gameObject);
        // Keep the Updater first, as the other members might want to register to it
        m_Updater = new Updater ();
        m_EventManager = new EventManager ();
    }

    void Update ()
    {
        m_Updater.Update ();
    }
}
