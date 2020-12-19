using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

namespace _16
{
    class Program
    {
        public static Regex fieldPattern = new Regex("(?<name>[\\w ]+): (?<r1>\\d+-\\d+) or (?<r2>\\d+-\\d+)");

        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");
            var fields = new List<Field>();

            var data = input.GetEnumerator();
            while (data.MoveNext() && !string.IsNullOrWhiteSpace(data.Current))
            {
                var matches = fieldPattern.Match(data.Current);
                var name = matches.Groups["name"].Value;
                var r1 = matches.Groups["r1"].Value.Split("-");
                var r2 = matches.Groups["r2"].Value.Split("-");

                fields.Add(new Field
                {
                    Name = name,
                    First = (int.Parse(r1.First()), int.Parse(r1.Last())),
                    Second = (int.Parse(r2.First()), int.Parse(r2.Last()))
                });
            }

            data.MoveNext(); // after - "your ticket":
            data.MoveNext(); // after - your ticket data
            var myTicket = data.Current.Split(",").Select(int.Parse).ToArray();

            data.MoveNext(); // after - new line
            data.MoveNext(); // after - "nearby tickets:"

            var otherTickets = new HashSet<int[]>();
            while (data.MoveNext())
            {
                otherTickets.Add(data.Current.Split(",").Select(int.Parse).ToArray());
            }

            Part1(fields, myTicket, otherTickets);
        }

        static void Part1(List<Field> fields, int[] myTicket, HashSet<int[]> tickets)
        {
            var sum = 0;
            foreach (var ticket in tickets)
            {
                foreach (var value in ticket)
                {
                    if (!fields.Any(
                        field => (value >= field.First.low && value <= field.First.high)
                            || (value >= field.Second.low && value <= field.Second.high)
                    ))
                    {
                        sum += value;
                    }
                }
            }

            Console.WriteLine(sum);
        }
    }

    public class Field
    {
        public string Name { get; set; }
        public (int low, int high) First { get; set; }
        public (int low, int high) Second { get; set; }
    }

}
