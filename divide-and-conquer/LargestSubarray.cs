using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace divide_and_conquer
{
    public sealed class LargestSubarray
    {
        public static int[] GetLargestSubarrayByRecursive(int[] numbers)
        {
            int sum = numbers[0], sumTemp, startIndex = 0, endIndex = 0;
            for (int i = 0; i < numbers.Length; i++)
            {
                sumTemp = 0;
                for (int j = i; j < numbers.Length; j++)
                {
                    sumTemp += numbers[j];
                    if (sumTemp > sum)
                    {
                        sum = sumTemp;
                        startIndex = i;
                        endIndex = j;
                    }
                }
            }
            return numbers[startIndex..(endIndex + 1)];
        }

        //2. 分治求解 
        public static int[] GetLargestSubarrayByDivide(int[] numbers)
        {
            var (startIndex, endIndex, sum) = GetLargestSubarray(0, numbers.Length - 1, numbers);
            return numbers[startIndex..(endIndex + 1)];
        }
        private static (int startIndex, int endIndex, int sum) GetLargestSubarray(int low, int high, int[] numbers)
        {
            if (low == high) return (low, high, numbers[low]);
            int mid = (low + high) / 2;
            var res1 = GetLargestSubarray(low, mid, numbers);
            var res2 = GetLargestSubarray(mid + 1, high, numbers);

            int startIndex = mid, endIndex = mid + 1, sum = numbers[mid], sumTemp = 0;
            for (int i = mid; i >= low; i--)
            {
                sumTemp += numbers[i];
                if (sumTemp > sum)
                {
                    startIndex = i;
                    sum = sumTemp;
                }
            }
            sumTemp = sum;
            for (int j = mid + 1; j <= high; j++)
            {
                sumTemp += numbers[j];
                if (sumTemp > sum)
                {
                    endIndex = j;
                    sum = sumTemp;
                }
            }
            if (res1.sum >= sum && res1.sum >= res2.sum)
            {
                return res1;
            }
            else if (res2.sum >= sum && res2.sum >= res1.sum)
            {
                return res2;
            }
            else
            {
                return (startIndex, endIndex, sum);
            }
        }
    }
}
