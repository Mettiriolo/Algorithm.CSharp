using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using multi_thread;

namespace Test
{
    [TestFixture]
    internal class UnitTest_multi_thread
    {
        [Test]
        public void JoinDemoTest()
        { 
            JoinDemo._t1.Start();
            JoinDemo._t2.Start();
        }
        [Test]
        public void SharedDemoTest()
        {
            SharedDemo sharedDemo1 = new SharedDemo();
            new Thread(sharedDemo1.Go).Start();
            sharedDemo1.Go();

            bool done = false;
            ThreadStart action = () =>
            {
                if (!done)
                {
                    done = true;
                    Console.WriteLine("Action");
                }
            };
            new Thread(action).Start();
            action();
        }

        [Test]
        public void SafeDemoTest()
        {
            SafeDemo safeDemo = new SafeDemo();
            //new Thread(safeDemo.UnLockGo).Start();
            //safeDemo.UnLockGo();
            //ThreadStart action = new ThreadStart(() =>
            //{
            //    if (!safeDemo._done)
            //    {
            //        Console.WriteLine("Done");
            //        Thread.Sleep(3000);
            //        safeDemo._done = true;
            //    }
            //});
            //new Thread(action).Start();
            //action();

            new Thread(safeDemo.Go).Start();
            safeDemo.Go();

        }

        [Test]
        public void ExceptionDemoTest()
        {
            try
            {
                new Thread(ExceptionDemo.Go).Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Test]
        public void BackgroundDemoTest()
        {
            BackgroundDemo.Test();
        }
        [Test]
        public void SignalDemoTest()
        {
            SignalDemo.Test();
        }

        [Test]
        public void TaskDemoTest()
        {
            TaskDemo.Test();
        }
    }
}
