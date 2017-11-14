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

        public void Walk(ConcurrentQueue<TreeComponent<string>> queue, int count = 4)
        {
            _q = queue;
            _count = count;
            start = DateTime.Now;

            for (int i = 0; i < count; i++)
            {
                var worker = new BackgroundWorker();
                worker.WorkerSupportsCancellation = true;

                worker.DoWork += new DoWorkEventHandler((object sender, DoWorkEventArgs args) => {

                    Console.WriteLine("Starting Worker ");

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
                    Console.WriteLine("End Worker");
                });
                worker.RunWorkerAsync();
                worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
                _workers.Add(worker);
            }
        }

        private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_count == _workers.Count(w => w.IsBusy == false))
            {
                Console.WriteLine("End Task");
                Console.WriteLine(" " + (DateTime.Now - start).TotalMilliseconds + $" / Visited {_visited}");
            }

        }
    }

}
