﻿using ProjectEuler.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectEuler
{
    partial class ProjectEuler
    {
        /// <summary>
        /// Truncates a number from the left side
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>A list of truncated results</returns>
        static List<int> getTruncateLeft(int n)
        {
            List<int> list = new List<int>();
            String s = n.ToString();
            while (s.Length > 0)
            {
                list.Add(Int32.Parse(s));
                s = s.Substring(1);
            }
            return list;
        }

        /// <summary>
        /// Truncates a number from the right side
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>A list of truncated results</returns>
        static List<int> getTruncateRight(int n)
        {

            List<int> list = new List<int>();
            String s = n.ToString();
            while (s.Length > 0)
            {
                list.Add(Int32.Parse(s));
                s = s.Substring(0, s.Length - 1);
            }
            return list;
        }


        /// <summary>
        ///  Calculates the sum of every two-sided truncatable prime
        /// </summary>
        static void P037()
        {
            int ans = 0;
            foreach (int p in (from i in Enumerable.Range(11, 750000) where !i.ToString().Contains('5') && Functions.isPrime(i) select i))
            {
                bool b = true;
                foreach (int i in getTruncateLeft(p).Concat(getTruncateRight(p)))
                {
                    if (Functions.isPrime(i)) continue;
                    else
                    {
                        b = false;
                        break;
                    }
                }
                if (b) ans += p;
            }
            Console.WriteLine(ans + 53);
        }
    }
}