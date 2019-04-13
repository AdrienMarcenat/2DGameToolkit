using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dialogue
{
    public class Option
    {
        public string m_Text;
        public int m_DestinationNodeID;

        public Option()
        { }

        public Option(string text, int destinationNodeID)
        {
            m_Text = text;
            m_DestinationNodeID = destinationNodeID;
        }
    }
}
