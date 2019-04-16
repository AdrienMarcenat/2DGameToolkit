using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DialogueOptionButton : MonoBehaviour
{
    private Dialogue.Option m_Option;
    private Text m_Text;

    private void Start()
    {
        m_Text = GetComponent<Text>();
    }

    public void OnOptionChosen()
    {
        Dialogue.DialogueManagerProxy.Get().DisplayNode(m_Option.m_DestinationNodeID);
    }

    public void SetOption(Dialogue.Option option)
    {
        m_Option = option;
        m_Text.text = m_Option.m_Text;
    }

    public void Reset()
    {
        m_Option = null;
        m_Option.m_Text = "";
    }
}
