using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace multi_thread
{
    public sealed class TaskDemo
    {
        public static void Test()
        {
            Task.Run(() => {
                for (int i = 0; i < 1000; i++)
                { 
                    Console.Write("X");
                    Task.Delay(1000);
                }
            });
            Task.Run(() => {
                for (int i = 0; i < 1000; i++)
                {
                    Console.Write("Y");
                    Task.Delay(1000);
                }
            });
            Task.Delay(5000);
        }
    }
}
