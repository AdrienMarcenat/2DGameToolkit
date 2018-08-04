using UnityEngine;

public class UniqueProxy<T> : ScriptableObject where T : class
{
    public static void Open (T instance)
    {
        Debug.Assert (ms_Instance == null, "Proxy already set !");
        ms_Instance = instance;
    }

    public static void Close ()
    {
        Debug.Assert (ms_Instance != null, "Proxy wasn't set");
        ms_Instance = null;
    }

    public static T Get ()
    {
        Debug.Assert (ms_Instance != null, "Invalid proxy");
        return ms_Instance;
    }

    public static bool IsValid()
    {
        return ms_Instance != null;
    }

    private static T ms_Instance;
}