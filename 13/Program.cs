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
            Part2(input[1].Split(',').Select(x => ulong.TryParse(x, out var i) ? i : (ulong?)null).ToArray());
        }

        static void Part1(int earliestTime, int[] busses)
        {
            var data = busses
                .Select(x => new { x, diff = (Math.Ceiling((double)earliestTime / x) * x) - earliestTime })
                .OrderBy(x => x.diff);
            var soonest = data.First();
            Console.WriteLine(soonest.x * soonest.diff);
        }

        static void Part2(ulong?[] busses)
        {
            var data =
                busses.Select(x => x == null ? null : new Bus(x.Value)).ToArray();
            var result = IsSolved(data);

            while (!result.isSolved)
            {
                ApplyNextIteration(data);
                result = IsSolved(data);
            }

            Console.WriteLine(data[0].Current);
        }

        static void ApplyNextIteration(Bus[] busses)
        {
            ulong minValue = 100000000000000;
            var reverse = busses.Reverse().ToArray();
            ulong max = 0;
            for (var i = 0; i < busses.Length; i++)
            {
                if (i == 0)
                {
                    reverse[i].Current += reverse[i].Id;
                    max = reverse[i].Current;

                    if(max < minValue)
                    {
                        reverse[i].Current = (ulong)Math.Ceiling((double)minValue / reverse[i].Id) * reverse[i].Id;
                        max = reverse[i].Current;
                    }
                }
                else if (reverse[i] != null)
                {
                    reverse[i].Current = (ulong)Math.Floor((double)(max - (ulong)i) / reverse[i].Id) * reverse[i].Id;
                }
            }
            //Print(busses);
        }

        static (bool isSolved, Bus incorrectBus) IsSolved(Bus[] busses)
        {
            var min = busses[0].Current;
            for (var i = (ulong)1; i < (ulong)busses.LongLength; i++)
            {
                if (busses[i] != null && busses[i].Current != min + i)
                {
                    return (false, busses[i]);
                }
            }

            return (true, null);
        }

        public class Bus
        {
            public Bus(ulong id)
            {
                Id = id;

                Current = 0;
            }
            public ulong Id { get; set; }
            public ulong Current { get; set; }
        }

        static void Print(Bus[] busses)
        {
            Console.WriteLine(string.Join(' ', busses.Select(x => x == null ? "x" : x.Current.ToString())));
        }
    }
}
