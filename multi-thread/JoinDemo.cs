using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_thread
{
    public sealed class JoinDemo
    {
        public static readonly Thread _t1, _t2;
        static JoinDemo()
        {
            _t1 = new Thread(ThreadProc)
            {
                Name = "t1"
            };
            _t2 = new Thread(ThreadProc)
            {
                Name = "t2"
            };
        }
        private static void ThreadProc()
        {
            Console.WriteLine($"\nCurrent thread : {Thread.CurrentThread.Name}");
            if (Thread.CurrentThread.Name == "t1" &&
                _t2.ThreadState != ThreadState.Unstarted)
                if (_t2.Join(2000)) //true:2秒后t2执行结束 false：超市
                    Console.WriteLine("t2 termminated!");
                else
                    Console.WriteLine("t2 timeout");
            Thread.Sleep(3000);
            Console.WriteLine($"\nCurrent thread : {Thread.CurrentThread.Name}");
            Console.WriteLine($"t1 : {_t1.ThreadState}");
            Console.WriteLine($"t2 : {_t2.ThreadState}");
        }
    }
}
