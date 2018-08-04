using UnityEngine;
using UnityEditor;

public class PlayerGameEvent : GameEvent
{
    public PlayerGameEvent () : base ("Player")
    {
    }

    public string GetMessage ()
    {
        return "This is a player message GameEvent";
    }
}

public class PlayerHealthGameEvent : GameEvent
{
    public PlayerHealthGameEvent () : base ("Player")
    {
    }

    public string GetMessage ()
    {
        return "This is a player health message GameEvent";
    }
}