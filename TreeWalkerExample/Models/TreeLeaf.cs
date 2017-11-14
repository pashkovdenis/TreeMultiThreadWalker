using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWalkerExample.Models
{
    public class TreeLeaf<T> : TreeComponent<T>
    {
        public TreeLeaf()
        {
            _childNodes = new HashSet<TreeComponent<T>>();
        }

        public override void AddChild(TreeComponent<T> child)
        {
            throw new InvalidOperationException("Leaf cannot contain other leafs ");
        }
    }
}
