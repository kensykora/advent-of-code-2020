using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace _14
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");
            Part1(input);
        }

        static (ulong ones, ulong zeroes) GetMask(string mask)
        {
            ulong ones = 0;
            ulong zeroes = ulong.MaxValue;
            for (var i = 0; i < mask.Length; i++)
            {
                var c = mask[i];
                if (c == '1')
                {
                    ones = ones | (ulong)1 << (35 - i);
                }
                else if (c == '0')
                {
                    zeroes = zeroes & (((ulong.MaxValue - 1) << (35 - i)) | (ulong.MaxValue - 1) >> 64 - (35 - i));
                }
            }

            return (ones, zeroes);
        }

        static void Part1(IEnumerable<string> file)
        {
            var values = new Dictionary<ulong, ulong>();
            var input = file.GetEnumerator();
            var regex = new Regex("mem\\[(?<address>\\d+)\\] = (?<value>\\d+)");
            (ulong ones, ulong zeroes) mask = (0, ulong.MaxValue);

            while (input.MoveNext())
            {
                if (input.Current.StartsWith("mask = "))
                {
                    mask = GetMask(input.Current.Substring("mask = ".Length));
                }
                else
                {
                    var matches = regex.Match(input.Current);
                    values[ulong.Parse(matches.Groups["address"].Value)] = (ulong.Parse(matches.Groups["value"].Value) | mask.ones) & mask.zeroes;
                }
            }

            Console.WriteLine(values.Values.Aggregate((sum, next) => sum + next));
        }
    }
}
