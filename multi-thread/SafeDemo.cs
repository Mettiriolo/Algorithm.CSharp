using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace multi_thread
{
    public sealed class SafeDemo
    {
        private bool _done;
        private readonly object _lock = new object();
        public void Go()
        {
            lock (_lock)
            {
                if (!_done)
                {
                    Console.WriteLine("Done");
                    Thread.Sleep(3000);
                    _done = true;
                }
            }
        }

        public void UnLockGo()
        {
            if (!_done)
            {
                Console.WriteLine("Done");
                Thread.Sleep(3000);
                _done = true;
            }
        }

    }
}
