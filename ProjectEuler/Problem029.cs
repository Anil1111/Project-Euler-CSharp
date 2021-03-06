﻿using System;
using System.Collections.Generic;

namespace ProjectEuler
{
    partial class ProjectEuler
    {
        /// <summary>
        /// Calculates the number of distinct terms in the sequence generated by a^b for 2 ≤ a ≤ 100 and 2 ≤ b ≤ 100
        /// </summary>
        static void P029()
        {
            HashSet<double> S = new HashSet<double>();
            for (int a = 2; a <= 100; a++)
                for (int b = 2; b <= 100; b++)
                    S.Add(Math.Pow(a, b));
            Console.WriteLine(S.Count);
        }
    }
}