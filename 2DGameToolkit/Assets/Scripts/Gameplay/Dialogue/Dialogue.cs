using System.Collections.Generic;

namespace Dialogue
{
    public class Dialogue
    {
        public List<Node> m_Nodes = new List<Node>();

        public Dialogue()
        { }

        public void AddNode(Node node)
        {
            m_Nodes.Add(node);
        }
    }
}

