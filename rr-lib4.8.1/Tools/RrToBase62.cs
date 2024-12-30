using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rrlib481.Tools
{
    public class RrToBase62
    {
        public static string GenId(int len = 10)
        {
            var rnd = new Random();
            string result = string.Empty;
            for (int i = 0; i < len; i++)
            {
                result += "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"[rnd.Next(0, 61)];
            }
            return result;
        }
        public static string ToBase62(int value)
        {
            string result = string.Empty;
            int baseNum = 62;
            int times = 1;
            int p = Convert.ToInt32(Math.Pow(baseNum, times));
            while (value >= p)
            {
                times++;
                p = Convert.ToInt32(Math.Pow(baseNum, times));
            }
            times--;
            for (int i = times; i >= 0; i--)
            {
                p = Convert.ToInt32(Math.Pow(baseNum, i));
                int q = value / p;
                result += "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"[q];
                value %= p;
            }
            return result;
        }
    }
}
