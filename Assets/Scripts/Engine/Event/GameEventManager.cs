using UnityEngine;
using System.Collections.Generic;

public class GameEventManager
{
    private Dictionary<string, ListenerNotifier> m_Notifiers;
    private Queue<GameEvent> m_GameEventQueue;
    private bool m_DispatchGuard = false;

    public GameEventManager ()
    {
        m_Notifiers = new Dictionary<string, ListenerNotifier> ();
        m_GameEventQueue = new Queue<GameEvent> ();
        UpdaterProxy.Get ().Register (this, EUpdatePass.First);
        GameEventManagerProxy.Open (this);
    }

    ~GameEventManager ()
    {
        GameEventManagerProxy.Close ();
    }

    public void Register (System.Object objectToNotify, string tag, params System.Type[] GameEventTypes)
    {
        Debug.Assert (!m_DispatchGuard, "Cannot register a listener while dispatching !");
        ListenerNotifier notifier = null;

        if (!m_Notifiers.TryGetValue (tag, out notifier))
        {
            notifier = new ListenerNotifier (tag);
            m_Notifiers.Add (tag, notifier);
        }

        Debug.Assert (notifier != null);
        notifier.AddListener (objectToNotify, tag, GameEventTypes);
    }

    public void Unregister (System.Object objectToNotify, string tag)
    {
        Debug.Assert (!m_DispatchGuard, "Cannot unregister a listener while dispatching !");
        ListenerNotifier notifier = null;

        if (!m_Notifiers.TryGetValue (tag, out notifier))
        {
            Debug.Assert (false, "Trying to unregister to GameEvents from " + tag + " but no notifier was found.");
        }

        notifier.RemoveListener (objectToNotify, tag);
    }

    public void PushGameEvent (GameEvent e, GameEvent.EProtocol protocol)
    {
        Debug.Assert (!m_DispatchGuard, "Cannot push GameEvent while dispatching !");
        switch (protocol)
        {
            case GameEvent.EProtocol.Delayed:
                m_GameEventQueue.Enqueue (e);
                break;
            case GameEvent.EProtocol.Instant:
                Notify (e);
                break;
            case GameEvent.EProtocol.Discard:
                break;
            default:
                Debug.Assert (false, "Invalid GameEvent protocol");
                break;
        }
    }

    public void UpdateFirst ()
    {
        m_DispatchGuard = true;
        while (m_GameEventQueue.Count != 0)
        {
            Notify (m_GameEventQueue.Dequeue ());
        }
        m_DispatchGuard = false;
    }

    private void Notify (GameEvent e)
    {
        string tag = e.GetTag ();
        ListenerNotifier notifier = null;

        if (!m_Notifiers.TryGetValue (tag, out notifier))
        {
            Debug.Assert (false, "No notifier foudn for tag " + tag);
        }

        notifier.Notify (e);
    }
}

class GameEventManagerProxy : UniqueProxy<GameEventManager>
{ }