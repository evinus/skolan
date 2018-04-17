using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mittbibliotek
{
    class TreeNode
    {

        public TreeNode LeftNode { get; set; }

        public int Data { get; set; }

        public TreeNode RightNode { get; set; }

        public TreeNode (int nodeData)
        {
            Data = nodeData;
            LeftNode = RightNode = null;
        }

        public void Insert( int insertvalue)
        {
            if(insertvalue < Data)
            {
                if (LeftNode == null)
                    LeftNode = new TreeNode(insertvalue);
                else
                    LeftNode.Insert(insertvalue);
            }
            else
            {
                if (RightNode == null)
                    RightNode = new TreeNode(insertvalue);
                else
                    RightNode.Insert(insertvalue);
            }
        }
    }
}
