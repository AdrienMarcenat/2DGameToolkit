using System;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class Connection
{
    public ConnectionPoint m_InPoint;
    public ConnectionPoint m_OutPoint;
    [XmlIgnore] readonly private Action<Connection> m_OnClickRemoveConnection;

    // parameterless constructo for xml serialization
    public Connection() { }

    public Connection(ConnectionPoint inPoint, ConnectionPoint outPoint, Action<Connection> OnClickRemoveConnection)
    {
        m_InPoint = inPoint;
        m_OutPoint = outPoint;
        m_OnClickRemoveConnection = OnClickRemoveConnection;
    }

    public void Draw()
    {
        Handles.DrawBezier(
            m_InPoint.GetRect().center,
            m_OutPoint.GetRect().center,
            m_InPoint.GetRect().center + Vector2.left * 50f,
            m_OutPoint.GetRect().center - Vector2.left * 50f,
            Color.white,
            null,
            2f
        );

        if (Handles.Button((m_InPoint.GetRect().center + m_OutPoint.GetRect().center) * 0.5f, Quaternion.identity, 4, 8, Handles.RectangleHandleCap))
        {
            if (m_OnClickRemoveConnection != null)
            {
                m_OnClickRemoveConnection(this);
            }
        }
    }
}