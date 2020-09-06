using System;

namespace Second
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please input the string to test");
            string input = Console.ReadLine();
            Console.WriteLine(IsBinaryStringGood(input));
        }

        static bool IsBinaryStringGood(string binary)
        {
            int countDiff = 0;
            foreach(char ch in binary)
            {
                if (ch == '1')
                    countDiff++;
                else if (ch == '0')
                    countDiff--;
                else
                    throw new Exception("Bad Binary String");

                if (countDiff < 0)
                    return false;
            }

            return countDiff == 0;
        }
    }
}
