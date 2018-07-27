public class Event
{
    private string m_Tag;

    public Event (string tag)
    {
        m_Tag = tag;
        EventManagerProxy.Get ().PushEvent (this);
    }

    public string GetTag ()
    {
        return m_Tag;
    }
}

public class PlayerEvent : Event
{
    public PlayerEvent () : base ("Player")
    {
    }

    public string GetMessage ()
    {
        return "This is a player message event";
    }
}

public class PlayerHealthEvent : Event
{
    public PlayerHealthEvent () : base ("Player")
    {
    }

    public string GetMessage ()
    {
        return "This is a player health message event";
    }
}