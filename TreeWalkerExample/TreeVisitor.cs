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

             
            // Some Dummy work 
            var res = Math.Sqrt( Math.PI *  new Random().Next(100)); 



        }
    }
}
