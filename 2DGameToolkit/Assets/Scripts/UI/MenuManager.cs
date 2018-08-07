using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] GameObject m_MainMenu;
    [SerializeField] GameObject m_BlinkingText;
    [SerializeField] float m_BlinkingRate;

    void Start ()
    {
        m_MainMenu.SetActive (false);
        StartCoroutine (BlinkRoutine ());
    }

    public void Update ()
    {
        if (Input.GetButtonDown("Escape"))
        {
            Quit ();
        }
        else if (Input.anyKeyDown)
        {
            StopAllCoroutines ();
            m_BlinkingText.SetActive (false);
            m_MainMenu.SetActive (true);
        }
    }

    public void Quit ()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
			    Application.Quit ();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene (1);
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
