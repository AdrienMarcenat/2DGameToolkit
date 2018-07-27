using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using AnyObject = System.Object;

public enum EUpdatePass
{
    First,
    BeforeAI,
    AfterAI,
    Last,
    // Do not move count !!!
    Count
}

public class Updater
{
    List<AnyObject>[] m_ObjectListPerPass;

    public Updater ()
    {
        m_ObjectListPerPass = new List<AnyObject>[(int)EUpdatePass.Count];
        for (int i = 0; i < (int)EUpdatePass.Count; i++)
        {
            m_ObjectListPerPass[i] = new List<AnyObject> ();
        }
        UpdaterProxy.Open (this);
    }

    private void OnDestroy ()
    {
        UpdaterProxy.Close ();
    }

    public void Update ()
    {
        if (m_IsPaused)
        {
            return;
        }

        for (int i = 0; i < (int)EUpdatePass.Count; i++)
        {
            EUpdatePass pass = (EUpdatePass)i;
            foreach (AnyObject objectToUpdate in m_ObjectListPerPass[(int)pass])
            {
                ReflectionHelper.CallMethod ("Update" + pass.ToString (), objectToUpdate);
            }
        }
    }

    public void Register (AnyObject objectToUpdate, params EUpdatePass[] updatePassList)
    {
        foreach (EUpdatePass pass in updatePassList)
        {
            Debug.Assert (pass != EUpdatePass.Count, "Invalid Update Pass : " + pass.ToString ());
            if (!m_ObjectListPerPass[(int)pass].Contains (objectToUpdate))
            {
                m_ObjectListPerPass[(int)pass].Add (objectToUpdate);
            }
        }
    }

    public void Unregister (AnyObject objectToUpdate, params EUpdatePass[] updatePassList)
    {
        foreach (EUpdatePass pass in updatePassList)
        {
            Debug.Assert (pass != EUpdatePass.Count, "Invalid Update Pass : " + pass.ToString ());
            m_ObjectListPerPass[(int)pass].Remove (objectToUpdate);
        }
    }

    public void SetPause (bool pause)
    {
        m_IsPaused = pause;
    }

    public bool IsPaused ()
    {
        return m_IsPaused;
    }

    private bool m_IsPaused = false;
}

public class UpdaterProxy : UniqueProxy<Updater>
{}