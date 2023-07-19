using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_thread
{
    public sealed class BackgroundDemo
    {
        public static void Test()
        {
            Thread thread = new Thread(() => {
                Console.WriteLine("Go to sleep...");
                Thread.Sleep(10000);
                Console.WriteLine("Wake up...");
            });
            thread.Start();
            thread.IsBackground = true;
        }
    }
}
