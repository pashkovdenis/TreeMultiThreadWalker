using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeWalkerExample.Models;

namespace TreeWalkerExample
{
    public class TreeVisitor<T> :  ITreeVisitor<T>
    { 
        
        public void Visit(TreeComponent<T> component)
        { 
            if (component == null)
                return; 
            component.Visited = true;
            for (int i = 0; i < 10m; i++)
            {
                var res = Math.Sqrt(Math.PI * (int.MaxValue));
            }
    
             
        }
    }
}
