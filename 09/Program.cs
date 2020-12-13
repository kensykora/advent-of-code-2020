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

            var target = Part1(input);
            Part2(input, target);
        }

        static ulong Part1(ulong[] input)
        {
            for (var i = 25; i < input.Length; i++)
            {
                if (!IsValid(input.Skip(i - 25).Take(25).ToArray(), input[i]))
                {
                    Console.WriteLine(input[i]);
                    return input[i];
                }
            }

            throw new ApplicationException("not found");
        }

        static void Part2(ulong[] input, ulong target)
        {
            int i = 0, j = 0;
            for (; i < input.Length; i++)
            {
                var sum = input[i];

                for (j = i + 1; j < input.Length; j++)
                {
                    sum += input[j];
                    if (sum >= target)
                    {
                        break;
                    }
                }

                if (sum == target)
                {
                    var range = input.Skip(i).Take(j - i + 1);
                    Console.WriteLine(range.Min() + range.Max());
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
