using UnityEngine;
using System.Collections;

public class EndLevelPanel : MonoBehaviour
{
    [SerializeField] GameObject m_EndLevelPanel;

    void Start ()
    {
        m_EndLevelPanel.SetActive (false);
    }

    void OnEnable ()
    {
        //GameFlowEndLevelState.EndLevel += EndLevel;
    }

    void OnDisable ()
    {
        //GameFlowEndLevelState.EndLevel -= EndLevel;
    }

    void EndLevel (bool pause)
    {
        m_EndLevelPanel.SetActive (pause);
    }
}

