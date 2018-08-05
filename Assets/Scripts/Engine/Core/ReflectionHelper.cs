using NUnit.Framework;
using System.Reflection;

public class ReflectionHelper
{
   public static void CallMethod (string methodName, System.Object instance, params System.Object[] args)
    {
        try
        {
            instance.GetType().InvokeMember (methodName, BindingFlags.InvokeMethod, null, instance, args);
        }
        catch (System.Exception mme)
        {
            Assert.Fail ("Method '" + methodName + "' was not found on type " + instance.GetType() + " (" + mme + ")");
        }
    }
}