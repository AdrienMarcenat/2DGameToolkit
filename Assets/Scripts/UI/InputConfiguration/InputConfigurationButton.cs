using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class InputConfigurationButton : MonoBehaviour
{
    private Text m_InputKeyCodeText;

    private bool m_WaitingForKey;

    void Start ()
    {
        m_WaitingForKey = false;
        m_InputKeyCodeText = GetComponent<Text> ();
    }

    public void StartAssignment (string inputName)
    {
        if (!m_WaitingForKey)
        {
            StartCoroutine (AssignKey (inputName));
        }
    }

    private IEnumerator AssignKey (string inputName)
    {
        m_WaitingForKey = true;

        Event keyEvent = Event.current;
        while (!keyEvent.isKey)
            yield return null;

        KeyCode newKeyCode = keyEvent.keyCode;
        InputManagerProxy.Get ().ChangeKeyCode (inputName, newKeyCode);
        m_InputKeyCodeText.text = newKeyCode.ToString ();

        m_WaitingForKey = false;
    }
}