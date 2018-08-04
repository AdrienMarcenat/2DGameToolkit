
public static class Extension
{
    public static void RegisterAsListener(this System.Object objectToNotify, string tag, params System.Type[] GameEventTypes)
    {
        GameEventManagerProxy.Get ().Register (objectToNotify, tag, GameEventTypes);
    }

    public static void UnregisterAsListener (this System.Object objectToNotify, string tag)
    {
        GameEventManagerProxy.Get ().Unregister (objectToNotify, tag);
    }

    public static void RegisterToUpdate (this System.Object objectToNotify, params EUpdatePass[] updatePassList)
    {
        UpdaterProxy.Get ().Register (objectToNotify, updatePassList);
    }

    public static void UnregisterToUpdate (this System.Object objectToNotify)
    {
        UpdaterProxy.Get ().Unregister (objectToNotify);
    }
}