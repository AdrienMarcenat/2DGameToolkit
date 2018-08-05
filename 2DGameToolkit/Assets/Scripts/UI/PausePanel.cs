using UnityEngine;
using System.Collections;

public class PausePanel : MonoBehaviour
{
    [SerializeField] GameObject m_PausePanel;

    void Start ()
    {
        m_PausePanel.SetActive (false);
    }

    void OnEnable ()
    {
        //GameFlowPauseState.Pause += Pause;
    }

    void OnDisable ()
    {
        //GameFlowPauseState.Pause -= Pause;
    }

    void Pause (bool pause)
    {
        m_PausePanel.SetActive (pause);
    }
}

