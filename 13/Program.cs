using System;
using System.IO;
using System.Linq;

namespace _13
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var earliestTime = int.Parse(input[0]);
            var busses = input[1].Split(',').Where(x => x != "x").Select(int.Parse).ToArray();

            Part1(earliestTime, busses);
        }

        static void Part1(int earliestTime, int[] busses)
        {
            var data = busses
                .Select(x => new { x, diff = (Math.Ceiling((double)earliestTime / x) * x) - earliestTime })
                .OrderBy(x => x.diff);
            var soonest = data.First();
            Console.WriteLine(soonest.x * soonest.diff);
        }
    }
}
