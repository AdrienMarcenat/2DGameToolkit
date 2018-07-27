using UnityEngine;
using System.Collections;

public abstract class HSMState : MonoBehaviour
{
    protected HSM m_HSM;
    protected int m_ID;

    protected virtual void Awake ()
    {
        m_HSM = GetComponent<HSM> ();
    }

    protected virtual void Start ()
    {
        m_HSM.RegisterState (m_ID, this);
    }

    public virtual void OnEnter () { }
    public virtual bool OnUpdate () { return false; }
    public virtual void OnExit () { }

    protected void RequestStackPush (int stateID)
    {
        m_HSM.PushState (stateID);
    }

    protected void RequestStackPop ()
    {
        m_HSM.PopState ();
    }

    protected void RequestStateClear ()
    {
        m_HSM.ClearStates ();
    }
}

