using UnityEngine;
using System.Collections.Generic;

public class EventManager
{
    private Dictionary<string, ListenerNotifier> m_Notifiers;
    private Queue<Event> m_EventQueue;

    public EventManager ()
    {
        m_Notifiers = new Dictionary<string, ListenerNotifier> ();
        m_EventQueue = new Queue<Event> ();
        UpdaterProxy.Get ().Register (this, EUpdatePass.First);
        EventManagerProxy.Open (this);
    }

    ~EventManager ()
    {
        EventManagerProxy.Close ();
    }

    public void Register(IListenerInterface listener)
    {
        ListenerNotifier notifier = null;
        string tag = listener.GetTag ();

        if (!m_Notifiers.TryGetValue (tag, out notifier))
        {
            notifier = new ListenerNotifier (tag);
            m_Notifiers.Add (tag, notifier);
        }

        Debug.Assert (notifier != null);
        notifier.AddListener (listener);
    }

    public void Unregister (IListenerInterface listener)
    {
        ListenerNotifier notifier = null;
        string tag = listener.GetTag ();

        if (!m_Notifiers.TryGetValue (tag, out notifier))
        {
            Debug.Assert (false, "Trying to unregister to events from " + tag + " but no notifier was found.");
        }

        notifier.RemoveListener (listener);
    }

    public void PushEvent(Event e)
    {
        m_EventQueue.Enqueue (e);
    }

    public void UpdateFirst ()
    {
        while(m_EventQueue.Count != 0)
        {
            Event e = m_EventQueue.Dequeue ();
            string tag = e.GetTag ();
            ListenerNotifier notifier = null;

            if (!m_Notifiers.TryGetValue (tag, out notifier))
            {
                Debug.Assert (false, "No notifier foudn for tag " + tag);
            }

            notifier.Notify (e);
        }
    }
}

class EventManagerProxy : UniqueProxy<EventManager>
{}