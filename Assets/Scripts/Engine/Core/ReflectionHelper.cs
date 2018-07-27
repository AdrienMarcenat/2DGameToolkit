using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

public class ReflectionHelper
{
   public static void CallMethod (string methodName, System.Object instance, params System.Object[] args)
    {
        try
        {
            instance.GetType().InvokeMember (methodName, BindingFlags.InvokeMethod, null, instance, args);
        }
        catch (System.MissingMethodException mme)
        {
            Debug.Assert (false, "Method '" + methodName + "' was not found on type " + instance.GetType() + " (" + mme + ")");
        }
    }
}