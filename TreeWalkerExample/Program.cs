using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeWalkerExample.Models;

namespace TreeWalkerExample
{
    class Program
    {
        private static TreeBranch<string> Root;
        private static int total = 0;
        private static int Visited = 0;  
        static void Main(string[] args)
        {
            Console.WriteLine("MultiThread Walker Example"); 

          
            //ThreadPool.SetMinThreads(2, 4); 
            ThreadPool.GetAvailableThreads(out int wokers, out int completion); 

         

            while (true)
            {
              
                Visited = 0;
                total = 0;
                Console.Clear(); 


                Root = new TreeBranch<string>();
                Root.AddChild(BuildTree(new TreeBranch<string>()));
               
                Console.WriteLine("Starting Walking Tree component");
                var start = DateTime.Now;

                var walker = new TreeRunner(new Action<TreeComponent<string>>((TreeComponent<string> cs) =>
                {
                    new TreeVisitor<string>().Visit(cs);
                    Interlocked.Increment(ref Visited);
                }
                ));

                var c = new ConcurrentQueue<TreeComponent<string>>();
                c.Enqueue(Root);
              
                walker.Walk(c);
                 

                Console.WriteLine("Tree Walker Finished " + (DateTime.Now - start).TotalMilliseconds + " / " + Visited);
                Console.ReadLine();
            }

        } 

        static TreeComponent<string> BuildTree( TreeComponent<string> c)
        {
            if (total < 800)
            {
                total++;
                for (int i = 0; i < (800); i++)
                {
                    c.AddChild(BuildTree(new  TreeBranch<string>())); 
                }
            }
            return c;
        }

    }







}
