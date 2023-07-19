using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace multi_thread
{
    public sealed class ExceptionDemo
    {
        public static void Go()
        { 
            throw new NotImplementedException("this is a notImplementedException");
        }
    }
}
