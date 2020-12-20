using System;
using System.Linq;
using System.Collections.Generic;

namespace _17
{
    class Program
    {
        static void Main(string[] args)
        {
            var sample = @".#.
                           ..#
                           ###";
            var input = @"..##.#.#
                          .#####..
                          #.....##
                          ##.##.#.
                          ..#...#.
                          .#..##..
                          .#...#.#
                          #..##.##";

            var startLines = input.Split('\n').Select(x => x.Trim()).ToList();
            var data = new Map();
            for (var i = 0; i < startLines.Count(); i++)
            {
                for (var j = 0; j < startLines[i].Length; j++)
                {
                    data[i][j][0] = startLines[i][j] == '#';
                }
            }

            Part1(data);
        }

        static void Part1(Map data)
        {
            var result = Process(data, 6);

            Console.WriteLine(result.CountActive());
        }

        static Map Process(Map data, int interations)
        {
            var currentMap = data;

            for (var current = 0; current < interations; current++)
            {
                var next = new Map();
                for (var i = currentMap.MinX - 1; i <= currentMap.MaxX + 1; i++)
                {
                    for (var j = currentMap.MinY - 1; j <= currentMap.MaxY + 1; j++)
                    {
                        for (var k = currentMap.MinZ - 1; k <= currentMap.MaxZ + 1; k++)
                        {
                            var activeNeighbors = currentMap.CountActiveNeighbors(i, j, k);
                            var isActive = currentMap.IsActive(i, j, k);
                            if (isActive && (activeNeighbors < 2 || activeNeighbors > 3))
                            {
                                isActive = false;
                            }
                            else
                            {
                                if (!isActive && activeNeighbors == 3)
                                {
                                    isActive = true;
                                }
                            }

                            next[i][j][k] = isActive;
                        }
                    }
                }

                currentMap = next;
            }

            return currentMap;
        }
    }
}
