using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_thread
{
    public sealed class SharedDemo
    {
        private bool _done;
        public void Go()
        {
            if (!_done)
            { 
                _done = true;
                Console.WriteLine("Done");
            }
        }
    }
}
