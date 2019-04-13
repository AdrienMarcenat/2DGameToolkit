using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogue
{
    public class Node
    {
        public int m_NodeID = -1;
        public string m_Name;
        public string m_Text;
        public List<Option> m_Options = new List<Option>();

        public Node()
        { }

        public Node(string name, string text)
        {
            m_Name = name;
            m_Text = text;
        }

        public void AddOption(Option option)
        {
            m_Options.Add(option);
        }
    }
}
