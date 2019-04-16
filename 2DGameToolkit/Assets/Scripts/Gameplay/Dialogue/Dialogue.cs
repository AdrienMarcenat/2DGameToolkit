using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Dialogue
{
    public class Dialogue
    {
        public Dictionary<string, Node> m_Nodes = new Dictionary<string, Node>();

        public Dialogue()
        { }

        public void AddNode(Node node)
        {
            m_Nodes.Add(node.m_ID, node);
        }

        public Node GetNode(string nodeID)
        {
            Node node;
            bool nodeExist = m_Nodes.TryGetValue(nodeID, out node);
            Assert.IsTrue(nodeExist, "Cannot find node with ID " + nodeID);
            return node;
        }

        public string GetRootNodeID()
        {
            return null;
        }
    }
}

