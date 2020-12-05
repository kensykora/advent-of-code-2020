using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace _03
{
    class Program
    {
        const int Right = 3;
        const int Down = 1;

        static void Main(string[] args)
        {
            Part1();
        }

        static void Part1()
        {
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

            for (var i = 0; i < map.Length-1; i++) // For each row, except the last
            {
                current.x += Right;
                current.y += Down;

                if(map[current.y][current.x % width])
                {
                    treesEncountered++;
                }
            }

            Console.WriteLine("Trees: " + treesEncountered);
        }
    }
}
