using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TreeWalkerExample.Models
{
    public interface ITreeVisitor<T>
    {
        void Visit(TreeComponent<T> component);
    }


}
