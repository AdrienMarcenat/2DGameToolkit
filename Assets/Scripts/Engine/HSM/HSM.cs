using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HSM : MonoBehaviour
{
    public enum Action
    {
        Push,
        Pop,
        Clear,
    };

    private struct PendingChange
    {
        public Action action;
        public int stateID;

        public PendingChange (Action a, int ID = 0)
        {
            action = a;
            stateID = ID;
        }
    };

    private Stack<HSMState> stateStack;
    private List<PendingChange> pendingList;
    private Hashtable factories;


    protected virtual void Awake ()
    {
        stateStack = new Stack<HSMState> ();
        pendingList = new List<PendingChange> ();
        factories = new Hashtable ();
    }

    public void RegisterState (int stateID, HSMState state)
    {
        factories.Add (stateID, state);
    }

    public void Update ()
    {
        foreach (HSMState state in stateStack)
        {
            if (state.OnUpdate ())
                break;
        }
        ApplyPendingChanges ();
    }


    public void PushState (int stateID)
    {
        pendingList.Add (new PendingChange (Action.Push, stateID));
    }

    public void PopState ()
    {
        if (IsEmpty ())
            return;
        pendingList.Add (new PendingChange (Action.Pop));
    }

    public void ClearStates ()
    {
        pendingList.Add (new PendingChange (Action.Clear));
    }

    public bool IsEmpty ()
    {
        return stateStack.Count == 0;
    }

    private HSMState FindState (int stateID)
    {
        return (HSMState)factories[stateID];
    }

    private void ApplyPendingChanges ()
    {
        foreach (PendingChange change in pendingList.ToArray ())
        {
            switch (change.action)
            {
                case Action.Push:
                    HSMState pushState = FindState (change.stateID);
                    pushState.OnEnter ();
                    stateStack.Push (pushState);
                    break;
                case Action.Pop:
                    HSMState popState = stateStack.Pop ();
                    popState.OnExit ();
                    break;
                case Action.Clear:
                    stateStack.Clear ();
                    break;
            }
        }
        pendingList.Clear ();
    }
}

