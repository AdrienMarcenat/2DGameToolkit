using System;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Node
{
    public Rect m_Rect;

    public ConnectionPoint m_InPoint;
    public ConnectionPoint m_OutPoint;

    [XmlIgnore] readonly private string m_Title;
    [XmlIgnore] private bool m_IsDragged;
    [XmlIgnore] private bool m_IsSelected;

    [XmlIgnore] private GUIStyle m_Style;
    [XmlIgnore] private readonly GUIStyle m_DefaultNodeStyle;
    [XmlIgnore] private readonly GUIStyle m_SelectedNodeStyle;

    [XmlIgnore] private Action<Node> m_OnRemoveNode;

    // parameterless constructo for xml serialization
    public Node()
    {
    }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint,
        Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode)
    {
        m_Rect = new Rect(position.x, position.y, width, height);
        m_Style = nodeStyle;
        m_InPoint = new ConnectionPoint(this, EConnectionPointType.In, inPointStyle, OnClickInPoint);
        m_OutPoint = new ConnectionPoint(this, EConnectionPointType.Out, outPointStyle, OnClickOutPoint);
        m_DefaultNodeStyle = nodeStyle;
        m_SelectedNodeStyle = selectedStyle;
        m_OnRemoveNode = OnClickRemoveNode;
    }

    public Node(Vector2 position, float width, float height, GUIStyle nodeStyle, GUIStyle selectedStyle, GUIStyle inPointStyle, GUIStyle outPointStyle, Action<ConnectionPoint> OnClickInPoint,
        Action<ConnectionPoint> OnClickOutPoint, Action<Node> OnClickRemoveNode, string inPointID, string outPointID)
    {
        m_Rect = new Rect(position.x, position.y, width, height);
        m_Style = nodeStyle;
        m_InPoint = new ConnectionPoint(this, EConnectionPointType.In, inPointStyle, OnClickInPoint, inPointID);
        m_OutPoint = new ConnectionPoint(this, EConnectionPointType.Out, outPointStyle, OnClickOutPoint, outPointID);
        m_DefaultNodeStyle = nodeStyle;
        m_SelectedNodeStyle = selectedStyle;
        m_OnRemoveNode = OnClickRemoveNode;
    }

    public void Drag(Vector2 delta)
    {
        m_Rect.position += delta;
    }

    public void Draw()
    {
        m_InPoint.Draw();
        m_OutPoint.Draw();
        GUI.Box(m_Rect, m_Title, m_Style);
    }

    public bool ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    GUI.changed = true;
                    if (m_Rect.Contains(e.mousePosition))
                    {
                        m_IsDragged = true;
                        m_IsSelected = true;
                        m_Style = m_SelectedNodeStyle;
                    }
                    else
                    {
                        m_IsSelected = false;
                        m_Style = m_DefaultNodeStyle;
                    }
                }

                if (e.button == 1 && m_IsSelected && m_Rect.Contains(e.mousePosition))
                {
                    ProcessContextMenu();
                    e.Use();
                }
                break;

            case EventType.MouseUp:
                m_IsDragged = false;
                break;

            case EventType.MouseDrag:
                if (e.button == 0 && m_IsDragged)
                {
                    Drag(e.delta);
                    e.Use();
                    return true;
                }
                break;
        }

        return false;
    }

    private void ProcessContextMenu()
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Remove node"), false, OnClickRemoveNode);
        genericMenu.ShowAsContext();
    }

    private void OnClickRemoveNode()
    {
        if (m_OnRemoveNode != null)
        {
            m_OnRemoveNode(this);
        }
    }
}