using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWalkerExample.Models
{
    public abstract class TreeComponent<T>
    {
        protected HashSet<TreeComponent<T>> _childNodes;

        protected T _data;
        public bool Visited { set; get; }

        public abstract void AddChild(TreeComponent<T> child);
        public HashSet<TreeComponent<T>> GetChilds() => _childNodes;
        public virtual void AcceptVisitor(Models.ITreeVisitor<T> visitor) => visitor.Visit(this);
    }
}
