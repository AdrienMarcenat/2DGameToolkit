using System;
using UnityEngine;

public class DialogueNode : Node
{
    private const float m_Margin = 10f;
    private const float m_NameFieldHeight = 20f;
    private const float m_TextFieldHeight = 100f;
    private const float m_Spacing = 10f;
    private readonly float m_TextFieldWidth;

    public Dialogue.Node m_Node = new Dialogue.Node("Name", "Text");

    public DialogueNode()
    {}

    public DialogueNode(Vector2 position, GUIStyle nodeStyle, GUIStyle selectedStyle
        , GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint
        , Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, string inPointID
        , string outPointID)
    : base(position, nodeStyle, selectedStyle, inPointStyle, outPointStyle
        , OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPointID, outPointID)
    {
        m_TextFieldWidth = m_Rect.width - 2 * m_Margin;
    }
    protected override float GetWidth()
    {
        return 200f;
    }
    protected override float GetHeight()
    {
        return 200f;
    }

    public override void Draw()
    {
        base.Draw();

        Rect nameRect = new Rect(m_Rect.x + m_Margin
            , m_Rect.y + m_Margin
            , m_TextFieldWidth
            , m_NameFieldHeight);
        m_Node.m_Name = GUI.TextField(nameRect, m_Node.m_Name);

        Rect textRect = new Rect(m_Rect.x + m_Margin
            , m_Rect.y + m_Margin + m_NameFieldHeight + m_Spacing
            , m_TextFieldWidth
            , m_TextFieldHeight);
        m_Node.m_Text = GUI.TextArea(textRect, m_Node.m_Text);
    }
}