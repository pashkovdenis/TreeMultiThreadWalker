using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWalkerExample.Models
{
    public class TreeBranch<T> : TreeComponent<T>
    {
        public TreeBranch()
        {
            _childNodes = new HashSet<TreeComponent<T>>();
        }

        public override void AddChild(TreeComponent<T> child) => _childNodes.Add(child);

    }

}
