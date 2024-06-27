using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A3_AVL
{
    internal class Node
    {
        public string Word { get; set; }
        public int ALength { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node()
        {
            Word = null;
            ALength = 0;
            Left = null;
            Right = null;
        }

        public Node(string word, int alength)
        {
            Word = word;
            ALength = alength;
            Left = null;
            Right = null;
        }

        public override string ToString()
        {
            return $"Word: {Word,-10}, Length: {ALength,-10}";
        }
    }
}
