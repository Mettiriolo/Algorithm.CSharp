using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_thread
{
    public sealed class SignalDemo
    {
        public static void Test()
        { 
            var signal = new ManualResetEvent(false);
            new Thread(() => {
                Console.WriteLine("Wait for signal ...");
                signal.WaitOne();
                signal.Dispose();
                Console.WriteLine("Go signal!");
            }).Start();
            Thread.Sleep(3000);
            Console.WriteLine("Main Thread...");
            signal.Set();
        }
    }
}
