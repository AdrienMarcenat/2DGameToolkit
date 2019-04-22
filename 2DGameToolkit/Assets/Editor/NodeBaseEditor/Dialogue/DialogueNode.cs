using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

public class DialogueNode : Node
{
    private const float m_Margin = 20f;
    private const float m_NameFieldHeight = 20f;
    private const float m_OptionFieldHeight = 20f;
    private const float m_TextFieldHeight = 100f;
    private const float m_Spacing = 10f;

    private readonly float m_TextFieldWidth;
    private readonly Action<ConnectionPoint> m_OnClickOutPoint;
    private readonly GUIStyle m_OutPointStyle;

    private Rect m_RectWithMargin;
    private Dictionary<string, Dialogue.Option> m_ConnectionPointToOption = new Dictionary<string, Dialogue.Option>();
    private List<ConnectionPoint> m_OptionConnectionPoints = new List<ConnectionPoint>();

    public Dialogue.Node m_Node = new Dialogue.Node("Name", "Text");
    public List<ConnectionPoint> m_OptionOutPoints = new List<ConnectionPoint>();

    public DialogueNode()
    {}

    public DialogueNode(Vector2 position, GUIStyle nodeStyle, GUIStyle selectedStyle
        , GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint
        , Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, string inPointID
        , string outPointID, string id = null)
    : base(position, nodeStyle, selectedStyle, inPointStyle, outPointStyle
        , OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPointID, outPointID, false, id)
    {
        m_RectWithMargin = new Rect (m_Margin, m_Margin, GetWidth () - 2 * m_Margin, GetHeight () - 2 * m_Margin);
        m_TextFieldWidth = m_Rect.width - 2 * m_Margin;
        m_OnClickOutPoint = OnClickOutPoint;
        m_OutPointStyle = outPointStyle;
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

        GUILayout.BeginArea (m_RectWithMargin);
        m_Node.m_Name = GUILayout.TextField(m_Node.m_Name);
        m_Node.m_Text = GUILayout.TextArea(m_Node.m_Text, GUILayout.ExpandHeight(true));
        if(GUILayout.Button("Add option"))
        {
            AddOption();
        }

        foreach(Dialogue.Option option in m_Node.m_Options)
        {
            option.m_Text = GUILayout.TextArea(option.m_Text);
        }
        GUILayout.EndArea ();
        foreach (ConnectionPoint optionConnectionPoint in m_OptionConnectionPoints)
        {
            optionConnectionPoint.Draw();
        }
    }

    private void ChangeHeight(float value)
    {
        m_Rect.height += value;
        m_RectWithMargin.height += value;
    }

    private void AddOption()
    {
        ConnectionPoint outPoint = new ConnectionPoint(this, EConnectionPointType.Out
            , m_OutPointStyle, m_OnClickOutPoint, m_Rect.height, false);
        // The option is created without connection, so it points to the exit node id, -1
        Dialogue.Option option = new Dialogue.Option("Option", "");
        m_Node.AddOption(option);
        m_ConnectionPointToOption.Add(outPoint.m_Id, option);
        m_OptionConnectionPoints.Add(outPoint);
        ChangeHeight(m_OptionFieldHeight);
    }

    private void RemoveOption(Dialogue.Option option)
    {
        ChangeHeight (-m_OptionFieldHeight);
        m_Node.RemoveOption(option);
        //m_OptionConnectionPoints.Remove();
        //m_ConnectionPointToOption.Remove(outPoint.m_Id, option);
    }

    private Dialogue.Option GetOptionFromConnection(Connection connection)
    {
        Dialogue.Option option;
        string connectionOutPointID = connection.m_OutPoint.m_Id;
        bool optionExist = m_ConnectionPointToOption.TryGetValue(connectionOutPointID, out option);
        Assert.IsTrue(optionExist, "Cannot find option from connection");
        return option;
    }
    public override void OnConnectionMade(Connection connection)
    {
        if (connection.m_InPoint == m_InPoint)
        {
            return;
        }
        if (connection.m_OutPoint == m_OutPoint)
        {
            m_Node.m_NextNodeID = connection.m_InPoint.GetNode().m_ID;
        }
        else
        {
            Dialogue.Option option = GetOptionFromConnection(connection);
            option.m_DestinationNodeID = connection.m_InPoint.GetNode().m_ID;
        }
    }
    public override void OnConnectionRemove(Connection connection)
    {
        if (connection.m_InPoint == m_InPoint)
        {
            return;
        }
        if (connection.m_OutPoint == m_OutPoint)
        {
            m_Node.m_NextNodeID = "";
        }
        else
        {
            Dialogue.Option option = GetOptionFromConnection(connection);
            // Now this option points to the exit node
            option.m_DestinationNodeID = "";
        }
    }
}