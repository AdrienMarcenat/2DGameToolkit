using UnityEngine;
using System.Collections.Generic;

public class ListenerNotifier
{
    private string m_Tag;
    private List<IListenerInterface> m_Listeners;

    public ListenerNotifier (string tag)
    {
        m_Tag = tag;
        m_Listeners = new List<IListenerInterface> ();
    }

    public void Notify (Event e)
    {
        Debug.Assert (e.GetTag () == m_Tag, "Event has tag " + e.GetTag () + " but notifier has tag " + m_Tag);
        foreach (IListenerInterface listener in m_Listeners)
        {
            if (listener.IsEventHandled (e))
            {
                ReflectionHelper.CallMethod ("OnEvent", listener, e);
            }
        }
    }

    public void AddListener (IListenerInterface listener)
    {
        m_Listeners.Add (listener);
    }

    public void RemoveListener (IListenerInterface listener)
    {
        m_Listeners.Remove (listener);
    }
}