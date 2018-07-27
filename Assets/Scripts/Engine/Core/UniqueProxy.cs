using UnityEngine;

public class UniqueProxy<T> : ScriptableObject where T : class
{
    public static void Open (T instance)
    {
        Debug.Assert (ms_Instance == null);
        ms_Instance = instance;
    }

    public static void Close ()
    {
        Debug.Assert (ms_Instance != null);
        ms_Instance = null;
    }

    public static T Get ()
    {
        Debug.Assert (ms_Instance != null);
        return ms_Instance;
    }

    private static T ms_Instance;
}