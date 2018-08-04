using UnityEngine;
using UnityEngine.UI;

public class InputConfigurationMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_ConfigurationButton;

    void Start ()
    {
        InputManager inputManager = InputManagerProxy.Get ();
        foreach (string inputName in inputManager.GetInputs ().Keys)
        {
            GameObject inputConfigurationButtonObject = Instantiate (m_ConfigurationButton);
            inputConfigurationButtonObject.transform.SetParent (transform, false);
            inputConfigurationButtonObject.GetComponentInChildren<Text> ().text = inputName;
            inputConfigurationButtonObject.GetComponentInChildren<Button> ().GetComponentInChildren<Text>().text = inputManager.GetInputs ()[inputName].ToString();
        }
    }
}