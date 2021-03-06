﻿using UnityEngine;

public class UnityLogger : ILogger
{
    public void Log (object message)
    {
        Debug.Log (message);
    }
    public void Warning(object message)
    {
        Debug.LogWarning(message);
    }
}