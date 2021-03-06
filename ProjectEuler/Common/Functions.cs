﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace ProjectEuler.Common
{
    /// <summary>
    /// Class of useful functions
    /// </summary>
    static class Functions
    {
        #region Helper Functions

        /// <summary>
        /// Gets the number of occurrences of a number in a list
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="n">Int</param>
        /// <returns>The number of occurrences of n in list</returns>
        public static int getOccurrenceOfValue(List<long> list, int n)
        {
            return (from temp in list where temp.Equals(n) select temp).Count();
        }

        /// <summary>
        /// Converts IEnumerable to HashSet
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="enumerable">IEnumerable</param>
        /// <returns>Returns HashSet</returns>
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> input)
        {
            HashSet<T> hs = new HashSet<T>();
            foreach (T item in input)
                hs.Add(item);
            return hs;
        }

        /// <summary>
        /// Gets the permutations of a series of items
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="input">IEnumerable</param>
        /// <param name="count">Int</param>
        /// <returns>All permutations of the input</returns>
        public static IEnumerable<IEnumerable<T>> getPermutations<T>(IEnumerable<T> input, int count)
        {
            if (count == 0) yield return new T[0];
            else
            {
                int i = 0;
                foreach (T startingElement in input)
                {
                    var remainingItems = AllExcept(input, i);
                    foreach (IEnumerable<T> permutationOfRemainder in getPermutations(remainingItems, count - 1))
                        yield return Concat<T>(new T[] { startingElement }, permutationOfRemainder);
                    i++;
                }
            }
        }

        /// <summary>
        /// getPermutations helper method
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="a">IEnumerable</param>
        /// <param name="b">IEnumerable</param>
        /// <returns>IEnumerable a concatenated with IEnumerable b</returns>
        public static IEnumerable<T> Concat<T>(IEnumerable<T> a, IEnumerable<T> b)
        {
            foreach (T item in a) { yield return item; }
            foreach (T item in b) { yield return item; }
        }

        /// <summary>
        /// getPermutations helper method
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="input">IEnumerable</param>
        /// <param name="indexToSkip">Int</param>
        /// <returns>The input except one index</returns>
        public static IEnumerable<T> AllExcept<T>(IEnumerable<T> input, int indexToSkip)
        {
            int index = 0;
            foreach (T item in input)
            {
                if (index != indexToSkip) yield return item;
                index ++;
            }
        }

        #endregion Helper Functions

        #region Boolean Functions

        /// <summary>
        /// Determines if string1 is an anagram of string2
        /// </summary>
        /// <param name="str1">String</param>
        /// <param name="str2">String</param>
        /// <returns>True if string1 is an anagram of string2</returns>
        public static bool isAnagram(string str1, string str2)
        {
            return String.Concat(str1.OrderBy(c => c)) == String.Concat(str2.OrderBy(c => c));
        }

        /// <summary>
        /// Determines if a string is a palindrome
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>True if string is a palindrome</returns>
        public static bool isPalindrome(string str)
        {
            return str == new String(str.ToCharArray().Reverse().ToArray());
        }

        /// <summary>
        /// Determines if a number is 1-9 pandigital
        /// </summary>
        /// <param name="n">Long</param>
        /// <returns>True if n is 1-9 pandigital</returns>
        public static bool isPandigital(long n)
        {
            String s = n.ToString();
            return s.Contains("1") && s.Contains("2") && s.Contains("3") && s.Contains("4") && s.Contains("5") && s.Contains("6") && s.Contains("7") && s.Contains("8") && s.Contains("9");
        }

        /// <summary>
        /// Determines if a number is prime
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>True if n is prime</returns>
        public static bool isPrime(int n)
        {
            if (n <= 1) return false;
            if (n <= 3) return true;
            if ((n % 2 == 0) || (n % 3 == 0)) return false;
            int i = 5;
            while (i * i <= n)
            {
                if ((n % i == 0) || (n % (i + 2) == 0)) return false;
                i += 6;
            }
            return true;
        }

        /// <summary>
        /// Determines if a number is smooth
        /// </summary>
        /// <param name="n">BigInteger</param>
        /// <param name="s">Int</param>
        /// <returns>True if n is s-smooth</returns>
        public static bool isSmooth(BigInteger n, int s)
        {
            foreach (long p in getPrimesList(s + 1))
                while (n % p == 0)
                    n /= p;
            return n == 1;
        }

        /// <summary>
        /// Determines if a number is square
        /// </summary>
        /// <param name="n">Long</param>
        /// <returns>True if n is a square</returns>
        public static bool isSquare(long n)
        {
            return Math.Sqrt(n) % 1 == 0;
        }

        /// <summary>
        /// Determines if a number is squarefree
        /// </summary>
        /// <param name="n">BigInteger</param>
        /// <param name="primesList">List</param>
        /// <returns>True if n is squarefree</returns>
        public static bool isSquarefree(BigInteger n, List<long> primes)
        {
            foreach (long p in primes)
                if (n % (p * p) == 0) return false;
            return true;
        }

        #endregion Boolean Functions

        #region Getter Functions

        /// <summary>
        /// Dictionary used to memoize the Factorial sequence
        /// </summary>
        static IDictionary<int, BigInteger> factDict = new Dictionary<int, BigInteger>();

        /// <summary>
        /// Gets n! of a number via memoization
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The Factorial of n</returns>
        public static BigInteger getFactorial(int n)
        {
            if (!factDict.ContainsKey(n))
                factDict[n] = getFactorialUncached(n);
            return factDict[n];
        }

        /// <summary>
        /// Gets n! of a number via recursion
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The Factorial of n</returns>
        public static BigInteger getFactorialUncached(int n)
        {
            if (n == 0)
                return 1;
            else
                return n * getFactorial(n - 1);
        }

        /// <summary>
        /// Dictionary used to memoize the Fibonacci sequence
        /// </summary>
        static IDictionary<int, BigInteger> fibDict = new Dictionary<int, BigInteger>();

        /// <summary>
        /// Gets the nth Fibonacci number via memoization
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The nth Fibonacci number</returns>
        public static BigInteger getFibonacci(int n)
        {
            if (!fibDict.ContainsKey(n))
                fibDict[n] = getFibonacciUncached(n);
            return fibDict[n];
        }

        /// <summary>
        /// Gets the nth Fibonacci number via recursion
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The nth Fibonacci number</returns>
        public static BigInteger getFibonacciUncached(int n)
        {
            if (n < 2)
                return n;
            else
                return getFibonacci(n - 1) + getFibonacci(n - 2);
        }

        /// <summary>
        /// Gets the nth Fibonacci number via Binet's Formula
        /// </summary>
        /// <param name="n">BigInteger</param>
        /// <returns>The nth Fibonacci number</returns>
        public static BigInteger getFibonacciBinet(int n)
        {
            return (BigInteger)((BigInteger.Pow((BigInteger)(1 + Math.Sqrt(5)), n) - BigInteger.Pow((BigInteger)(1 - Math.Sqrt(5)), n)) / (BigInteger.Pow((BigInteger)2, n) * (BigInteger)Math.Sqrt(5)));
        }

        /// <summary>
        /// Gets a list of primes less than a number
        /// </summary>
        /// <param name="limit">Long</param>
        /// <returns>A list of primes less than the limit</returns>
        public static List<long> getPrimesList(long limit)
        {
            List<long> primes = new List<long>();
            bool[] sieve = new bool[limit];
            for (long i = 2; i < limit; i++) sieve[i] = true;
            for (long i = 2; i < limit; i++)
                if (sieve[i])
                {
                    primes.Add(i);
                    for (long j = i * i; j < limit; j += i)
                        sieve[j] = false;
                }
            return primes;
        }

        /// <summary>
        /// Gets a dictionary of primes less than a number
        /// </summary>
        /// <param name="limit">Int</param>
        /// <returns>A dictionary of primes less than the limit</returns>
        public static IDictionary<long, bool> getPrimesDict(int limit)
        {
            IDictionary<long, bool> primesDict = new Dictionary<long, bool>();
            bool[] sieve = new bool[limit];
            for (long i = 2; i < limit; i++) sieve[i] = true;
            for (long i = 2; i < limit; i++)
                if (sieve[i])
                {
                    primesDict.Add(i, true);
                    for (long j = i * i; j < limit; j += i)
                        sieve[j] = false;
                }
            return primesDict;
        }

        /// <summary>
        /// Gets the sorted factors of a number
        /// </summary>
        /// <param name="n">Long</param>
        /// <returns>A sorted set of the factors of n</returns>
        public static SortedSet<long> getFactors(long n)
        {
            SortedSet<long> factors = new SortedSet<long>();
            for (int i = 1; i <= Math.Sqrt(n); i++)
                if (n % i == 0)
                {
                    factors.Add(i);
                    factors.Add(n / i);
                }
            return factors;
        }

        /// <summary>
        /// Gets the prime factorization of a number
        /// </summary>
        /// <param name="n">Long</param>
        /// <returns>A list of the prime factors of n</returns>
        public static List<long> getPrimeFactors(long n)
        {
            List<long> primeFactors = new List<long>();
            int i = 2;
            while (i * i <= n)
            {
                if (n % i != 0)
                    i++;
                else
                {
                    n /= i;
                    primeFactors.Add(i);
                }
            }
            if (n > 1)
                primeFactors.Add(n);
            return primeFactors;
        }

        /// <summary>
        /// Gets the prime factorization of a number with occurrences
        /// </summary>
        /// <param name="n">Long</param>
        /// <returns>A dictionary of the prime factors of n and the number of occurrences</returns>
        public static IDictionary<long, int> getOmega(long n)
        {
            IDictionary<long, int> factorsDict = new Dictionary<long, int>();
            List<long> primeFactors = Functions.getPrimeFactors(n);
            foreach (int p in ToHashSet(primeFactors))
                factorsDict[p] = Functions.getOccurrenceOfValue(primeFactors, p);
            return factorsDict;
        }

        /// <summary>
        /// Gets the totient function of a number via Euler's product formula
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The totient function of n</returns>
        public static int getPhi(long n)
        {
            return (int)(from i in new HashSet<long>(Functions.getPrimeFactors(n)) select 1 - 1 / (double)i).Aggregate((double)n, (a, x) => a * x);
        }

        /// <summary>
        /// Gets the totient functions of numbers from 1 to n
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The totient functions of numbers from 1 to n</returns>
        public static IEnumerable<int> getTotients(int n)
        {
            int[] phi = new int[n + 1];
            phi[1] = 1;
            for (int i = 1; i <= n; i++)
            {
                if (phi[i] == 0)
                {
                    phi[i] = i - 1;
                    for (int j = 2; j <= n / i; j++)
                    {
                        if (phi[j] != 0)
                        {
                            int q = j;
                            int f = i - 1;
                            while (q % i == 0)
                            {
                                f *= i;
                                q /= i;
                            }
                            phi[i * j] = f * phi[q];
                        }
                    }
                }
                yield return phi[i];
            }
        }

        /// <summary>
        /// Gets n Choose k
        /// </summary>
        /// <param name="n">Int</param>
        /// <param name="k">Int</param>
        /// <returns>The binomial coefficient</returns>
        public static BigInteger getnCk(int n, int k)
        {
            BigInteger r = 1;
            if (k > n)
                return 0;
            for (long d = 1; d <= k; d++)
            {
                r *= n--;
                r /= d;
            }
            return r;
        }

        /// <summary>
        /// Gets the distance between two points
        /// </summary>
        /// <param name="x1">Double</param>
        /// <param name="y1">Double</param>
        /// <param name="x2">Double</param>
        /// <param name="y2">Double</param>
        /// <returns>The distance between (x1,y1) and (x2,y2)</returns>
        public static double getDistance(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(Math.Pow(x2 - x1, 2) + Math.Pow(y2 - y1, 2));
        }

        /// <summary>
        /// Gets the Greatest Common Divisor of two numbers
        /// </summary>
        /// <param name="a">Long</param>
        /// <param name="b">Long</param>
        /// <returns>The GCD of a and b</returns>
        public static long getGCD(long a, long b)
        {
            return b == 0 ? a : getGCD(b, a % b);
        }

        /// <summary>
        /// Gets the Least Common Multiple of two numbers
        /// </summary>
        /// <param name="a">Long</param>
        /// <param name="b">Long</param>
        /// <returns>The LCM of a and b</returns>
        public static long getLCM(long a, long b)
        {
            return a * b / getGCD(a, b);
        }

        /// <summary>
        /// Gets g, a, and b, such that ax + by = g, via the Extended Euclidean Algorithm
        /// </summary>
        /// <param name="a">Int</param>
        /// <param name="b">Int</param>
        /// <returns>The Extended Euclidean Algorithm: g (the divisor), a, and b, such that a * x + b * y = g</returns>
        public static Tuple<int, int, int> getExtendedGCD(int a, int b)
        {
            if (a == 0) return Tuple.Create(b, 0, 1);
            Tuple<int, int, int> egcd = getExtendedGCD(b % a, a);
            return Tuple.Create(egcd.Item1, egcd.Item3 - b / a * egcd.Item2, egcd.Item2);
        }

        /// <summary>
        /// Gets Modular Inverse of a mod m
        /// </summary>
        /// <param name="a">Int</param>
        /// <param name="m">Int</param>
        /// <returns>The Modular Inverse of a and m: ax % m == 1</returns>
        public static int getModInverse(int a, int m)
        {
            Tuple<int, int, int> egcd = getExtendedGCD(a, m);
            if (egcd.Item1 != 1) throw new System.ArgumentException("Mod Inverse does not exist", "a%m");
            return egcd.Item2 % m;
        }

        /// <summary>
        /// Gets the area of a triangle via Heron's Formula
        /// </summary>
        /// <param name="a">Double</param>
        /// <param name="b">Double</param>
        /// <param name="c">Double</param>
        /// <returns>The area of triangle abc</returns>
        public static double getHeron(double a, double b, double c)
        {
            double s = (a + b + c) / 2;
            return Math.Sqrt(s * (s - a) * (s - b) * (s - c));
        }


        /// <summary>
        /// Gets the sum of the squares of 1 to a number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The sum of squares of 1 to n</returns>
        public static int getSumOfSquares(int n)
        {
            return n * (n + 1) * (2 * n + 1) / 6;
        }

        /// <summary>
        /// Gets the probability of the leading digit of a set of numbers with large orders of magnitude via Benford's Law
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The probability of the leading digit n (1-9) of a set of numbers with large orders of magnitude</returns>
        public static double getBenford(int n)
        {
            return Math.Log10(1 + 1 / n);
        }

        /// <summary>
        /// Gets Triangle number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th triangle number</returns>
        public static long getTriangle(int n)
        {
            return n * (n + 1) / 2;
        }

        /// <summary>
        /// Gets Square number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th square number</returns>
        public static long getSquare(int n)
        {
            return n * n;
        }

        /// <summary>
        /// Gets Pentagon number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th pentagon number</returns>
        public static long getPentagon(int n)
        {
            return (long)n * (3 * n - 1) / 2;
        }

        /// <summary>
        /// Gets Hexagon number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th hexagon number</returns>
        public static long getHexagon(int n)
        {
            return n * (2 * n - 1);
        }

        /// <summary>
        /// Gets Heptagon number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th heptagon number</returns>
        public static long getHeptagon(int n)
        {
            return n * (5 * n - 3) / 2;
        }

        /// <summary>
        /// Gets Octagon number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The n-th octagon number</returns>
        public static long getOctagon(int n)
        {
            return n * (3 * n - 2);
        }

        /// <summary>
        /// Gets the squarefree numbers from a list of primes
        /// </summary>
        /// <param name="maxPrime">Int</param>
        /// <param name="product">Long</param>
        /// <param name="primeIndex">Int</param>
        /// <param name="primesList">List</param>
        /// <returns>The squarefree numbers from a list of primes</returns>
        public static IEnumerable<long> getSquarefrees(int maxPrime, long product, int primeIndex, List<long> primes)
        {
            long prime = primes[primeIndex];
            if (prime < maxPrime)
            {
                foreach (long value in getSquarefrees(maxPrime, product, primeIndex + 1, primes))
                    yield return value;
                foreach (long value in getSquarefrees(maxPrime, product * prime, primeIndex + 1, primes))
                    yield return value;
            }
            else
            {
                yield return product;
                yield return product * maxPrime;
            }
        }

        /// <summary>
        /// Gets the first (x,y) solution of Pell's equation: x^2 - D * y^2 = 1
        /// </summary>
        /// <param name="D">Long</param>
        /// <returns>The first solution of a given Pell's equation</returns>
        public static Tuple<BigInteger, BigInteger> getPellSolution(long D)
        {
            BigInteger uCur = 0;
            BigInteger vCur = 1;
            BigInteger a0 = (BigInteger)Math.Sqrt(D);
            BigInteger aCur = a0;
            BigInteger pCur = a0;
            BigInteger qCur = 1;
            BigInteger pPrev = 1;
            BigInteger qPrev = 0;
            int count = 0;
            while (aCur <= a0)
            {
                BigInteger uNext = aCur * vCur - uCur;
                BigInteger vNext = (D - BigInteger.Pow(uNext, 2)) / vCur;
                BigInteger aNext = (a0 + uNext) / vNext;
                BigInteger pNext = aNext * pCur + pPrev;
                BigInteger qNext = aNext * qCur + qPrev;
                pPrev = pCur;
		        qPrev = qCur;
		        uCur = uNext;
		        vCur = vNext;
		        aCur = aNext;
		        pCur = pNext;
                qCur = qNext;
                count++;
            }
            return count % 2 == 0 ? Tuple.Create(pPrev, qPrev) : Tuple.Create(BigInteger.Pow(pPrev, 2) + D * BigInteger.Pow(qPrev, 2), 2 * pPrev * qPrev);
        }

        /// <summary>
        /// Gets an array containing the Moebius Function values from 1 to n
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>An array containing the Moebius Function values from 1 to n</returns>
        public static int[] getMoebius(int n)
        {
            int[] mu = new int[n+1];
            for (int i = 0; i <= n; i++) mu[i] = 1;
            for (int i = 2; i <= Math.Sqrt(n); i++)
            {
                if (mu[i] == 1)
                {
                    for (int j = i; j <= n; j += i)
                        mu[j] *= -i;
                    for (int j = i*i; j <= n; j += i*i)
                        mu[j] *= 0;
                }
            }
            for (int i = 2; i <= n; i++)
            {
                if (mu[i] == i)
                    mu[i] = 1;
                else if (mu[i] == -i)
                    mu[i] = -1;
                else if (mu[i] < 0)
                    mu[i] = 1;
                else if (mu[i] > 0)
                    mu[i] = -1;
            }
            return mu;
        }

        /// <summary>
        /// Gets the Multiplicative Order of a modulo n
        /// </summary>
        /// <param name="a">Int</param>
        /// <param name="n">Int</param>
        /// <returns>Smallest k such that a^k = 1 (mod n)</returns>
        public static int getMultiplicativeOrder(int a, int n)
        {
            if (Functions.getGCD(a, n) != 1)
                return -1;
            int r = 1;
            int k = 1;
            while (k < n)
            {
                r = r * a % n;
                if (r == 1)
                    return k;
                k++;
            }
            return -1;
        }

        /// <summary>
        /// Gets the radical of a number
        /// </summary>
        /// <param name="n">Int</param>
        /// <returns>The radical of n</returns>
        public static int getRadical(int n)
        {
            return (int)(from i in new HashSet<long>(Functions.getPrimeFactors(n)) select i).Aggregate((long)1, (a, x) => a * x);
        }

        /// <summary>
        /// Gets the solution to a system of simultaneous linear congruences via the Chinese Remainder Theorem
        /// </summary>
        /// <param name="relativePrimes">List</param>
        /// <param name="solutions">List</param>
        /// <returns>The minimum solution to a system of simultaneous linear congruences</returns>
        public static int getChineseRemainderTheorem(List<int> relativePrimes, List<int> solutions)
        {
            if (relativePrimes.Count <= 1) throw new System.ArgumentException("Length of array of relative primes must be greater than one");
            if (((from i in relativePrimes from j in relativePrimes.GetRange(relativePrimes.IndexOf(i) + 1, relativePrimes.Count - relativePrimes.IndexOf(i) - 1) select Functions.getGCD(i, j)).Max() != 1)) throw new System.ArgumentException("Array of relative primes is not a collection of pairwise relatively prime integers");
            if (relativePrimes.Count != solutions.Count) throw new System.ArgumentException("Array of relative primes and array of solutions are not the same length");
            int M = (int)relativePrimes.Aggregate((long)1, (a, x) => a * x);
            int[] mArray = (from i in relativePrimes select M / i).ToArray();
            int[] yArray = (from i in relativePrimes select Functions.getModInverse(mArray[relativePrimes.IndexOf(i)] % i, i)).ToArray();
            return (from i in solutions.Zip(mArray, (x, y) => x * y).Zip(yArray, (x, y) => x * y) select i).Sum() % M;
        }

        #endregion Getter Functions
    }
}
