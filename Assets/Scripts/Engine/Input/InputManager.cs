using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class PlayerInputGameEvent : GameEvent
{
    public PlayerInputGameEvent (string input) : base ("Player", EProtocol.Instant)
    {
        m_Input = input;
    }

    public string GetInput ()
    {
        return m_Input;
    }

    private string m_Input;
}

public class InputManager
{
    private Dictionary<string, KeyCode> m_KeyCodes;
    private static string ms_InputFileName = "Datas/Input.txt";

    public InputManager ()
    {
        this.RegisterToUpdate (EUpdatePass.BeforeAI);
        m_KeyCodes = new Dictionary<string, KeyCode> ();
        FillKeyCodes (ms_InputFileName);
    }

    public void UpdateBeforeAI()
    {
        foreach(string inputName in m_KeyCodes.Keys)
        {
            if(Input.GetKeyDown(m_KeyCodes[inputName]))
            {
               new PlayerInputGameEvent (inputName).Push();
            }
        }
    }

    public Dictionary<string, KeyCode> GetInputs ()
    {
        return m_KeyCodes;
    }

    public void ChangeKeyCode(string inputName, KeyCode newKeyCode)
    {
        if (m_KeyCodes.ContainsKey (inputName))
        {
            m_KeyCodes[inputName] = newKeyCode;
        }
        else
        {
            Debug.Assert (false, "Cannot find input " + inputName);
        }
    }

    private void FillKeyCodes (string filename)
    {
        char[] separators = { ':' };
#if UNITY_EDITOR
        filename = "Assets/" + filename;
#endif

        string[] lines = File.ReadAllLines (filename);

        for (int i = 0; i < lines.Length; i++)
        {
            string[] datas = lines[i].Split (separators);

            // If there is an error in print a debug message
            if (datas.Length != 2)
            {
                Debug.Log ("Invalid number of data line " + i + " expecting 2, got " + datas.Length);
                return;
            }

            string inputName = datas[0];
            KeyCode inputKeyCode = (KeyCode)System.Enum.Parse (typeof (KeyCode), datas[1]);
            m_KeyCodes.Add (inputName, inputKeyCode);
        }
    }
}

public class InputManagerProxy : UniqueProxy<InputManager>
{
}