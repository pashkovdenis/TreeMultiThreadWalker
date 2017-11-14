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
    class TreeRunner
    {


        protected Action<TreeComponent<string>> _action;
        public TreeRunner(Action<TreeComponent<string>> action) => _action = action;
         
        public void Walk(ConcurrentQueue<TreeComponent<string>> queue, int count = 4)
        {

           

            using (var countDown = new CountdownEvent(count))
            {
                var b = new Barrier(count);

                for (int i = 0; i < count; i++)
                    ThreadPool.QueueUserWorkItem((object o) =>
                    {
                        b.SignalAndWait();
                        Console.WriteLine($"THread start {o}");
                        try
                        { 
                            while (queue.Count > 0)
                            {

                                if (queue.TryDequeue(out TreeComponent<string> node))
                                {
                                        // Do Work 
                                        _action.Invoke(node);
                                        foreach (var child in node.GetChilds())
                                            queue.Enqueue(child); 

                                }
                            } 

                        }
                        catch (Exception)
                        { 
                        }
                        finally
                        {
                            Console.WriteLine($"THread END  {o} final  ");
                            b.SignalAndWait();
                            countDown.Signal();
                        } 

                    }, i);

                countDown.Wait();
            }

        }









    }
}
