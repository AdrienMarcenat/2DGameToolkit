using UnityEngine;
using UnityEditor;
using System;
using System.IO;

public class DialogueNodeEditor : NodeBasedEditor<DialogueNodeEditor, DialogueNode, Connection>
    , INodeEditor<DialogueNode, Connection>
{
    [MenuItem("Window/Dialogue Editor")]
    private static void OpenWindow()
    {
        DialogueNodeEditor window = GetWindow<DialogueNodeEditor>();
        window.titleContent = new GUIContent("Dialogue Editor");
    }

    public virtual DialogueNode CreateNode(Vector2 position
       , GUIStyle nodeStyle
       , GUIStyle selectedStyle
       , GUIStyle inPointStyle
       , GUIStyle outPointStyle
       , Action<ConnectionPoint> OnClickInPoint
       , Action<ConnectionPoint> OnClickOutPoint
       , Action<Node> OnClickRemoveNode
       , string inPointID
       , string outPointID)
    {
        return new DialogueNode(position, nodeStyle, selectedStyle, inPointStyle, outPointStyle,
            OnClickInPoint, OnClickOutPoint, OnClickRemoveNode, inPointID, outPointID);
    }

    public virtual DialogueNode DeserializeNode(DialogueNode nodeDeserialized)
    {
        DialogueNode node = new DialogueNode(nodeDeserialized.m_Rect.position,
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
        node.m_Node.m_Name = nodeDeserialized.m_Node.m_Name;
        node.m_Node.m_Text = nodeDeserialized.m_Node.m_Text;
        return node;
    }

    public virtual Connection CreateConnection(Connection connection)
    {
        return connection;
    }

    protected override void Save(string savePath)
    {
        base.Save(savePath);
        // When the game will want to read the dialogue it does not care about visual stuff
        // so we serialize the gameplay info in another file in StreamingAssets
        Dialogue.Dialogue dialogue = new Dialogue.Dialogue();
        foreach (DialogueNode node in m_Graph.m_Nodes)
        {
            dialogue.AddNode(node.m_Node);
        }
        string filename = Path.GetFileName(savePath);
        XMLSerializerHelper.Serialize(dialogue, Application.streamingAssetsPath + "/Dialogues/" + filename);
    }
}