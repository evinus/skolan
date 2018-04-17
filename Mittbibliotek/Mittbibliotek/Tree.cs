using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mittbibliotek
{
    class Tree
    {
        private TreeNode root;

        public Tree()
        {
            root = null;
        }

        public void InsertNode(int insertvalue)
        {
            if (root == null)
                root = new TreeNode(insertvalue);
            else
                root.Insert(insertvalue);
        }
    }
}
