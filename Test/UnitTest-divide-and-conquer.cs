using divide_and_conquer;
using multi_thread;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    internal class UnitTest_divide_and_conquer
    {
        [Test]
        public void LargestSubarray1Test()
        {
            int[] numbers = { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };
            Console.WriteLine(string.Join(",", LargestSubarray.GetLargestSubarrayByDivide(numbers)));
        }

        [Test]
        public void LargestSubarray2Test()
        {
            int[] numbers = { 13, -3, -25, 20, -3, -16, -23, 18, 20, -7, 12, -5, -22, 15, -4, 7 };
            Console.WriteLine(string.Join(",", LargestSubarray.GetLargestSubarrayByRecursive(numbers)));
        }
    }
}
