using System;
using System.IO;
using System.Linq;

namespace _09
{
    class Program
    {
        static void Main(string[] args)
        {
            var input =
                File.ReadAllLines("input.txt")
                .Select(x => ulong.Parse(x))
                .ToArray();

            Part1(input);
        }

        static void Part1(ulong[] input)
        {
            for (var i = 25; i < input.Length; i++)
            {
                if (!IsValid(input.Skip(i - 25).Take(25).ToArray(), input[i]))
                {
                    Console.WriteLine(input[i]);
                    return;
                }
            }
        }

        static bool IsValid(ulong[] preamble, ulong value)
        {
            var first = 0;
            var second = first + 1;
            do
            {
                if (preamble[first] + preamble[second] == value)
                    return true;

                second++;
                if (second == preamble.Length)
                {
                    first++;
                    second = first + 1;
                }
            } while (first < preamble.Length - 1);

            return false;
        }
    }
}
