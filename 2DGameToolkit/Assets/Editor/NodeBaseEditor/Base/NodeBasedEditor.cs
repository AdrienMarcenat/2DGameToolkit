﻿using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public interface INodeEditor<NodeType, ConnectionType>
{
    NodeType CreateNode(Node node);
    ConnectionType CreateConnection(Connection connection);
    string GetSavePath();
}

public class Graph<NodeType, ConnectionType>
{
    public Graph()
    {
        m_Nodes = new List<NodeType>();
        m_Connections = new List<ConnectionType>();
    }

    public List<NodeType> m_Nodes;
    public List<ConnectionType> m_Connections;
}

public class NodeBasedEditor<Editor, NodeType, ConnectionType> : EditorWindow
    where Editor : NodeBasedEditor<Editor, NodeType, ConnectionType>, INodeEditor<NodeType, ConnectionType>
    where NodeType : Node, new()
    where ConnectionType : Connection, new()
{
    protected Graph<NodeType, ConnectionType> m_Graph;

    protected GUIStyle m_NodeStyle;
    protected GUIStyle m_SelectedNodeStyle;
    protected GUIStyle m_InPointStyle;
    protected GUIStyle m_OutPointStyle;

    protected ConnectionPoint m_SelectedInPoint;
    protected ConnectionPoint m_SelectedOutPoint;

    private Vector2 m_Offset;
    private Vector2 m_Drag;

    private const float m_MenuBarHeight = 20f;
    private Rect m_MenuBar;

    private Editor GetAsFinalType() { return (Editor)this; }
    private void OnEnable()
    {
        m_Graph = new Graph<NodeType, ConnectionType>();

        m_NodeStyle = new GUIStyle();
        m_NodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1.png") as Texture2D;
        m_NodeStyle.border = new RectOffset(12, 12, 12, 12);

        m_SelectedNodeStyle = new GUIStyle();
        m_SelectedNodeStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/node1 on.png") as Texture2D;
        m_SelectedNodeStyle.border = new RectOffset(12, 12, 12, 12);

        m_InPointStyle = new GUIStyle();
        m_InPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left.png") as Texture2D;
        m_InPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn left on.png") as Texture2D;
        m_InPointStyle.border = new RectOffset(4, 4, 12, 12);

        m_OutPointStyle = new GUIStyle();
        m_OutPointStyle.normal.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right.png") as Texture2D;
        m_OutPointStyle.active.background = EditorGUIUtility.Load("builtin skins/darkskin/images/btn right on.png") as Texture2D;
        m_OutPointStyle.border = new RectOffset(4, 4, 12, 12);
    }

    private void OnGUI()
    {
        DrawGrid(20, 0.2f, Color.gray);
        DrawGrid(100, 0.4f, Color.gray);
        DrawMenuBar();

        DrawNodes();
        DrawConnections();

        DrawConnectionLine(Event.current);

        ProcessNodeEvents(Event.current);
        ProcessEvents(Event.current);

        if (GUI.changed)
            Repaint();
    }

    private void DrawMenuBar()
    {
        m_MenuBar = new Rect(0, 0, position.width, m_MenuBarHeight);

        GUILayout.BeginArea(m_MenuBar, EditorStyles.toolbar);
        GUILayout.BeginHorizontal();

        if (GUILayout.Button(new GUIContent("Save"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            Save();
        }

        GUILayout.Space(5);

        if (GUILayout.Button(new GUIContent("Load"), EditorStyles.toolbarButton, GUILayout.Width(35)))
        {
            Load();
        }

        GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    private void DrawGrid(float gridSpacing, float gridOpacity, Color gridColor)
    {
        int widthDivs = Mathf.CeilToInt(position.width / gridSpacing);
        int heightDivs = Mathf.CeilToInt(position.height / gridSpacing);

        Handles.BeginGUI();
        Handles.color = new Color(gridColor.r, gridColor.g, gridColor.b, gridOpacity);

        m_Offset += m_Drag * 0.5f;
        Vector3 newOffset = new Vector3(m_Offset.x % gridSpacing, m_Offset.y % gridSpacing, 0);

        for (int i = 0; i < widthDivs; i++)
        {
            Handles.DrawLine(new Vector3(gridSpacing * i, -gridSpacing, 0) + newOffset, new Vector3(gridSpacing * i, position.height, 0f) + newOffset);
        }

        for (int j = 0; j < heightDivs; j++)
        {
            Handles.DrawLine(new Vector3(-gridSpacing, gridSpacing * j, 0) + newOffset, new Vector3(position.width, gridSpacing * j, 0f) + newOffset);
        }

        Handles.color = Color.white;
        Handles.EndGUI();
    }

    private void DrawNodes()
    {
        foreach (NodeType node in m_Graph.m_Nodes)
        {
            node.Draw();
        }
    }

    private void DrawConnections()
    {
        for(int i = m_Graph.m_Connections.Count-1; i >= 0; i--)
        {
            m_Graph.m_Connections[i].Draw();
        }
    }

    private void ProcessEvents(Event e)
    {
        m_Drag = Vector2.zero;

        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 0)
                {
                    ClearConnectionSelection();
                }

                if (e.button == 1)
                {
                    ProcessContextMenu(e.mousePosition);
                }
                break;

            case EventType.MouseDrag:
                if (e.button == 0)
                {
                    OnDrag(e.delta);
                }
                break;
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        foreach (NodeType node in m_Graph.m_Nodes)
        {
            GUI.changed |= node.ProcessEvents(e);
        }
    }

    private void DrawConnectionLine(Event e)
    {
        if (m_SelectedInPoint != null && m_SelectedOutPoint == null)
        {
            Handles.DrawBezier(
                m_SelectedInPoint.GetRect().center,
                e.mousePosition,
                m_SelectedInPoint.GetRect().center + Vector2.left * 50f,
                e.mousePosition - Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }

        if (m_SelectedOutPoint != null && m_SelectedInPoint == null)
        {
            Handles.DrawBezier(
                m_SelectedOutPoint.GetRect().center,
                e.mousePosition,
                m_SelectedOutPoint.GetRect().center - Vector2.left * 50f,
                e.mousePosition + Vector2.left * 50f,
                Color.white,
                null,
                2f
            );

            GUI.changed = true;
        }
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu genericMenu = new GenericMenu();
        genericMenu.AddItem(new GUIContent("Add node"), false, () => OnClickAddNode(mousePosition));
        genericMenu.ShowAsContext();
    }

    private void OnDrag(Vector2 delta)
    {
        m_Drag = delta;

        foreach (NodeType node in m_Graph.m_Nodes)
        {
            node.Drag(delta);
        }

        GUI.changed = true;
    }

    private void OnClickAddNode(Vector2 mousePosition)
    {
        Node baseNode = new Node(mousePosition
            , 200
            , 50
            , m_NodeStyle
            , m_SelectedNodeStyle
            , m_InPointStyle
            , m_OutPointStyle
            , OnClickInPoint
            , OnClickOutPoint
            , OnClickRemoveNode
        );
        m_Graph.m_Nodes.Add(GetAsFinalType().CreateNode(baseNode));
    }

    private void OnClickPoint()
    {
        if (m_SelectedOutPoint != null && m_SelectedInPoint != null)
        {
            if (m_SelectedOutPoint.GetNode() != m_SelectedInPoint.GetNode())
            {
                CreateConnection();
                ClearConnectionSelection();
            }
            else
            {
                ClearConnectionSelection();
            }
        }
    }

    protected void OnClickInPoint(ConnectionPoint inPoint)
    {
        m_SelectedInPoint = inPoint;
        OnClickPoint();
    }

    protected void OnClickOutPoint(ConnectionPoint outPoint)
    {
        m_SelectedOutPoint = outPoint;
        OnClickPoint();
    }

    protected void OnClickRemoveNode(Node node)
    {
        List<ConnectionType> connectionsToRemove = new List<ConnectionType>();

        foreach (ConnectionType connection in m_Graph.m_Connections)
        {
            if (connection.m_InPoint == node.m_InPoint || connection.m_OutPoint == node.m_OutPoint)
            {
                connectionsToRemove.Add(connection);
            }
        }

        foreach (ConnectionType connection in connectionsToRemove)
        {
            m_Graph.m_Connections.Remove(connection);
        }

        m_Graph.m_Nodes.Remove((NodeType)node);
    }

    private void OnClickRemoveConnection(Connection connection)
    {
        m_Graph.m_Connections.Remove((ConnectionType)connection);
    }

    private void CreateConnection()
    {
        Connection connectionBase = new Connection(m_SelectedInPoint, m_SelectedOutPoint, OnClickRemoveConnection);
        m_Graph.m_Connections.Add(GetAsFinalType().CreateConnection(connectionBase));
    }

    private void ClearConnectionSelection()
    {
        m_SelectedInPoint = null;
        m_SelectedOutPoint = null;
    }

    private void Save()
    {
        XMLSerializerHelper.Serialize(m_Graph, GetAsFinalType().GetSavePath());
    }

    private void Load()
    {
        Graph<NodeType, ConnectionType> graphDeserialized = XMLSerializerHelper.Deserialize<Graph<NodeType, ConnectionType>>(GetAsFinalType().GetSavePath());
        
        m_Graph.m_Nodes = new List<NodeType>();
        m_Graph.m_Connections = new List<ConnectionType>();

        foreach (var nodeDeserialized in graphDeserialized.m_Nodes)
        {
            Node nodeBase = new Node(
                nodeDeserialized.m_Rect.position,
                nodeDeserialized.m_Rect.width,
                nodeDeserialized.m_Rect.height,
                m_NodeStyle,
                m_SelectedNodeStyle,
                m_InPointStyle,
                m_OutPointStyle,
                OnClickInPoint,
                OnClickOutPoint,
                OnClickRemoveNode,
                nodeDeserialized.m_InPoint.m_Id,
                nodeDeserialized.m_OutPoint.m_Id
                );
            m_Graph.m_Nodes.Add(GetAsFinalType().CreateNode(nodeBase));
        }

        foreach (var connectionDeserialized in graphDeserialized.m_Connections)
        {
            var inPoint = m_Graph.m_Nodes.First(n => n.m_InPoint.m_Id == connectionDeserialized.m_InPoint.m_Id).m_InPoint;
            var outPoint = m_Graph.m_Nodes.First(n => n.m_OutPoint.m_Id == connectionDeserialized.m_OutPoint.m_Id).m_OutPoint;
            Connection connectionBase = new Connection(inPoint, outPoint, OnClickRemoveConnection);
            m_Graph.m_Connections.Add(GetAsFinalType().CreateConnection(connectionBase));
        }
    }
}