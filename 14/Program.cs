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
            Part2(input);
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

        static IEnumerable<(ulong ones, ulong zeroes)> GetMasks(string mask)
        {
            ulong ones = 0;
            ulong zeroes = ulong.MaxValue;
            var xIndicies = new List<int>();
            for (var i = 0; i < mask.Length; i++)
            {
                var c = mask[i];
                if (c == '1')
                {
                    ones = ones | (ulong)1 << (35 - i);
                }
                else if (c == '0')
                {
                    // 0's leaves as-is
                }
                else
                {
                    xIndicies.Add(35 - i);
                }
            }

            var masks = new List<(ulong ones, ulong zeroes)>();
            CreateMasks(masks, (ones, zeroes), xIndicies);

            return masks;
        }

        static void CreateMasks(List<(ulong ones, ulong zeroes)> masks, (ulong ones, ulong zeroes) rootMask, List<int> indicies)
        {
            var i = indicies.First();

            var zeroRoot = (rootMask.ones, rootMask.zeroes & (((ulong.MaxValue - 1) << i) | (ulong.MaxValue - 1) >> 64 - i));
            var oneRoot = (rootMask.ones | ((ulong)1) << i, rootMask.zeroes);

            if (indicies.Count == 1)
            {
                masks.Add(zeroRoot);
                masks.Add(oneRoot);
            }
            else
            {
                CreateMasks(masks, zeroRoot, indicies.Skip(1).ToList());
                CreateMasks(masks, oneRoot, indicies.Skip(1).ToList());
            }
        }

        static void Part2(IEnumerable<string> file)
        {
            var values = new Dictionary<ulong, ulong>();
            var input = file.GetEnumerator();
            var regex = new Regex("mem\\[(?<address>\\d+)\\] = (?<value>\\d+)");
            IEnumerable<(ulong ones, ulong zeroes)> masks = null;

            while (input.MoveNext())
            {
                if (input.Current.StartsWith("mask = "))
                {
                    masks = GetMasks(input.Current.Substring("mask = ".Length));
                }
                else
                {
                    var matches = regex.Match(input.Current);
                    var address = ulong.Parse(matches.Groups["address"].Value);
                    var value = ulong.Parse(matches.Groups["value"].Value);

                    foreach (var mask in masks)
                    {
                        var maskedAddress = (address | mask.ones) & mask.zeroes;
                        values[maskedAddress] = value;
                    }
                }
            }

            Console.WriteLine(values.Values.Aggregate((sum, next) => sum + next));
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
