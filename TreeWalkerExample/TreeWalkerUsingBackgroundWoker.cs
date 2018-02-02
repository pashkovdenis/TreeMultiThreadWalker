using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TreeWalkerExample.Models;

namespace TreeWalkerExample
{

    class TreeWalkerUsingBackgroundWoker
    {
        protected Action<TreeComponent<string>> _action;
        private DateTime start;
        public TreeWalkerUsingBackgroundWoker(Action<TreeComponent<string>> action) => _action = action;
        private List<BackgroundWorker> _workers = new List<BackgroundWorker>();
        private ConcurrentQueue<TreeComponent<string>> _q;
        private int _count = 0;
        private int _visited = 0;
        private List<AutoResetEvent> _resets = new List<AutoResetEvent>();


        public void  Walk(ConcurrentQueue<TreeComponent<string>> queue, int count = 4)
        {

            
                _q = queue;
                _count = count;
                start = DateTime.Now;
                   
                for (int i = 0; i < count; i++)
                {
                    
                
                    var reset = new AutoResetEvent(false);
                    _resets.Add(reset); 


                    ThreadPool.QueueUserWorkItem((object o) => {
                         
                        while (_q.Count > 0)
                        {
                            if (_q.TryDequeue(out TreeComponent<string> node))
                            {
                                _action.Invoke(node);
                                Interlocked.Increment(ref _visited);   
                                foreach (var child in node.GetChilds())
                                            _q.Enqueue(child); 
                            }
                        } 
                        ((AutoResetEvent)o).Set(); 
                    },reset); 
                     


                }
                WaitHandle.WaitAll(_resets.ToArray());
         
        }


        public void Worker_RunWorkerCompleted()
        {
            Console.WriteLine("End Task");
            Console.WriteLine(" " + (DateTime.Now - start).TotalMilliseconds + $" / Visited {_visited}");
        }
    }

}
