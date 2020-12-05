using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03
{
    class Program
    {

        static void Main(string[] args)
        {
            //Part1();
            Part2();
        }

        static void Part1()
        {
            const int Right = 3;
            const int Down = 1;

            var input = File.ReadLines("input.txt");
            var rows = new List<bool[]>();
            foreach (var line in input)
            {
                rows.Add(line.Select(x => x == '#').ToArray());
            }

            var map = rows.ToArray();

            var current = (x: 0, y: 0);
            var width = map[0].Length;
            var treesEncountered = 0;

            for (var i = 0; i < map.Length - 1; i++) // For each row, except the last
            {
                current.x += Right;
                current.y += Down;

                if (map[current.y][current.x % width])
                {
                    treesEncountered++;
                }
            }

            Console.WriteLine("Trees: " + treesEncountered);
        }

        static void Part2()
        {
            var input = File.ReadLines("input.txt");
            var rows = new List<bool[]>();
            foreach (var line in input)
            {
                rows.Add(line.Select(x => x == '#').ToArray());
            }

            var map = rows.ToArray();
            var width = map[0].Length;

            var runs = new List<SlopeRun>()
            {
                new SlopeRun { Right = 1, Down = 1 },
                new SlopeRun { Right = 3, Down = 1 },
                new SlopeRun { Right = 5, Down = 1 },
                new SlopeRun { Right = 7, Down = 1 },
                new SlopeRun { Right = 1, Down = 2 }
            };

            foreach (var run in runs)
            {
                while (run.CurrentY + run.Down < map.Length)
                {
                    run.CurrentX += run.Right;
                    run.CurrentY += run.Down;

                    if (map[run.CurrentY][run.CurrentX % width])
                    {
                        run.TreesEncountered++;
                    }
                }
            }

            Console.WriteLine("Result: " + runs.Aggregate(
                (result, item) => new SlopeRun() { TreesEncountered = result.TreesEncountered * item.TreesEncountered }).TreesEncountered);
        }
    }

    public class SlopeRun
    {

        public int CurrentX { get; set; }
        public int CurrentY { get; set; }
        public int Right { get; set; }
        public int Down { get; set; }
        public uint TreesEncountered { get; set; }
    }
}
