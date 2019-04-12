using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

public class TestNodeEditor : NodeBasedEditor<TestNodeEditor, Node, Connection>
    , INodeEditor<Node, Connection>
{
    [MenuItem("Window/Node Based Editor test")]
    private static void OpenWindow()
    {
        TestNodeEditor window = GetWindow<TestNodeEditor>();
        window.titleContent = new GUIContent("Node Based Editor test");
    }

    public virtual Node CreateNode(Node node)
    {
        return node;
    }
    public virtual Connection CreateConnection(Connection connection)
    {
        return connection;
    }
    public virtual string GetSavePath() { return "Assets/StreamingAssets/Test.xml"; }
}