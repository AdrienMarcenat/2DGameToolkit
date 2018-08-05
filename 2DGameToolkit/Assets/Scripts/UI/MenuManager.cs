using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_BlinkingText;
    [SerializeField] float m_BlinkingRate;

    void Start ()
    {
        StartCoroutine (BlinkRoutine ());
        this.RegisterAsListener ("Player", typeof (PlayerInputGameEvent));
    }

    private void OnDestroy ()
    {
        this.UnregisterAsListener ("Player");
    }

    public void OnGameEvent (PlayerInputGameEvent inputEvent)
    {
        string input = inputEvent.GetInput ();
        EInputState state = inputEvent.GetInputState ();
        switch (input)
        {
            case "Escape":
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
			    Application.Quit ();
#endif
                break;
            case "Jump":
                StopAllCoroutines ();
                SceneManager.LoadScene (1);
                break;
            default:
                break;
        }
    }

    IEnumerator BlinkRoutine ()
    {
        while (true)
        {
            BlinkText ();
            yield return new WaitForSeconds (m_BlinkingRate);
        }
    }

    private void BlinkText ()
    {
        m_BlinkingText.SetActive (!m_BlinkingText.activeSelf);
    }
}
