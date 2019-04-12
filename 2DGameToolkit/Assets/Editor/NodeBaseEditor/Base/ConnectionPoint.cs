using System;
using System.Xml.Serialization;
using UnityEngine;

public enum EConnectionPointType
{
    In,
    Out
}

public class ConnectionPoint
{
    public string m_Id;

    [XmlIgnore] private Rect m_Rect;
    [XmlIgnore] private readonly EConnectionPointType m_Type;
    [XmlIgnore] private readonly Node m_Node;
    [XmlIgnore] private readonly GUIStyle m_Style;
    [XmlIgnore] private readonly Action<ConnectionPoint> m_OnClickConnectionPoint;

    // parameterless constructo for xml serialization
    public ConnectionPoint() { }

    public ConnectionPoint(Node node, EConnectionPointType type, GUIStyle style, Action<ConnectionPoint> OnClickConnectionPoint, string id = null)
    {
        m_Node = node;
        m_Type = type;
        m_Style = style;
        m_OnClickConnectionPoint = OnClickConnectionPoint;
        m_Rect = new Rect(0, 0, 10f, 20f);

        m_Id = id ?? Guid.NewGuid().ToString();
    }

    public Rect GetRect()
    {
        return m_Rect;
    }

    public Node GetNode()
    {
        return m_Node;
    }

    public void Draw()
    {
        m_Rect.y = m_Node.m_Rect.y + (m_Node.m_Rect.height * 0.5f) - m_Rect.height * 0.5f;

        switch (m_Type)
        {
            case EConnectionPointType.In:
                m_Rect.x = m_Node.m_Rect.x - m_Rect.width + 8f;
                break;

            case EConnectionPointType.Out:
                m_Rect.x = m_Node.m_Rect.x + m_Node.m_Rect.width - 8f;
                break;
        }

        if (GUI.Button(m_Rect, "", m_Style))
        {
            if (m_OnClickConnectionPoint != null)
            {
                m_OnClickConnectionPoint(this);
            }
        }
    }
}