using UnityEngine;

public interface IListenerInterface
{
    string GetTag ();
    bool IsEventHandled (Event e);
}

public class Listener : IListenerInterface
{
    private string m_Tag;
    private System.Type[] m_EventTypes;

    public Listener (string tag, params System.Type[] eventTypes)
    {
        m_Tag = tag;
        m_EventTypes = eventTypes;
    }

    protected void RegisterAsListener ()
    {
        EventManagerProxy.Get ().Register (this);
    }

    protected void UnregisterAsListener ()
    {
        EventManagerProxy.Get ().Unregister (this);
    }

    public string GetTag ()
    {
        return m_Tag;
    }

    public bool IsEventHandled (Event e)
    {
        System.Type eventType = e.GetType ();
        foreach (System.Type type in m_EventTypes)
        {
            if (type == eventType)
            {
                return true;
            }
        }
        return false;
    }
}

public class MonoBehaviourListener : MonoBehaviour, IListenerInterface
{
    private Listener m_Listener;

    protected void CreateListener(string tag, params System.Type[] eventTypes)
    {
        m_Listener = new Listener (tag, eventTypes);
    }

    protected void RegisterAsListener ()
    {
        EventManagerProxy.Get ().Register (this);
    }

    protected void UnregisterAsListener ()
    {
        EventManagerProxy.Get ().Unregister (this);
    }

    public string GetTag ()
    {
        return m_Listener.GetTag ();
    }

    public bool IsEventHandled (Event e)
    {
        return m_Listener.IsEventHandled (e);
    }
}