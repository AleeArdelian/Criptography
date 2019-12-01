using System;
using System.Collections.Generic;

namespace Problem6
{
    class Program
    {
        static int modularExp(int x, int y, int p) //(x^y)mod p
        {
            int res = 1;
            x = x % p;

            while (y > 0)
            {
                // If y is odd, multiply x with result 
                if ((y & 1) == 1)
                    res = (res * x) % p;

                // y is even now 
                y = y >> 1;
                x = (x * x) % p;
            }
            return res;
        }

        //static int modularExp(int A, int B, int C)
        //{

        //    // Base cases  
        //    if (A == 0)
        //        return 0;
        //    if (B == 0)
        //        return 1;

        //    // If B is even  
        //    long y;
        //    if (B % 2 == 0)
        //    {
        //        y = modularExp(A, B / 2, C);
        //        y = (y * y) % C;
        //    }

        //    // If B is odd  
        //    else
        //    {
        //        y = A % C;
        //        y = (y * modularExp(A, B - 1,
        //                             C) % C) % C;
        //    }

        //    return (int)((y + C) % C);
        //}

        static bool checkIfPseudoPrime(int n, int b)
        {
            if (gcd(b, n) != 1)
                return false;
            if (modularExp(b, n - 1, n) != 1)
                return false;
            return true;
        }

        static int gcd(int a, int b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                    a %= b;
                else
                    b %= a;
            }
            return a == 0 ? b : a;
        }

        public static List<int> allBases(int n)
        {
            List<int> allBases = new List<int>();
            //every nr is pseudoprime to a=1 and a=n-1
            allBases.Add(1);

            for (int bas = 2; bas < n - 1; bas++)
            {
                if (checkIfPseudoPrime(n, bas))
                    allBases.Add(bas);
            }
            allBases.Add(n - 1);
            return allBases;
        }
        static void Main(string[] args)
        {
            Console.Write("Give n: ");
            int nr = Convert.ToInt32(Console.ReadLine());

            while (nr != 0)
            {
                List<int> bases = allBases(nr);
                foreach (int b in bases)
                    Console.Write(" " + b + "\n");

                Console.Write("Give n: ");
                nr = Convert.ToInt32(Console.ReadLine());
            }

        }
    }
}
