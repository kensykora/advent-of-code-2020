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
            var data4d = new Map4d();
            for (var i = 0; i < startLines.Count(); i++)
            {
                for (var j = 0; j < startLines[i].Length; j++)
                {
                    data[i][j][0] = startLines[i][j] == '#';
                    data4d[i][j][0][0] = startLines[i][j] == '#';
                }
            }



            //Part1(data);
            Part2(data4d);
        }

        static void Part1(Map data)
        {
            var result = Process(data, 6);

            Console.WriteLine(result.CountActive());
        }

        static void Part2(Map4d data)
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

        static Map4d Process(Map4d data, int interations)
        {
            var currentMap = data;

            for (var current = 0; current < interations; current++)
            {
                var next = new Map4d();
                for (var i = currentMap.MinX - 1; i <= currentMap.MaxX + 1; i++)
                {
                    for (var j = currentMap.MinY - 1; j <= currentMap.MaxY + 1; j++)
                    {
                        for (var k = currentMap.MinZ - 1; k <= currentMap.MaxZ + 1; k++)
                        {
                            for (var l = currentMap.MinQ - 1; l <= currentMap.MaxQ + 1; l++)
                            {
                                var activeNeighbors = currentMap.CountActiveNeighbors(i, j, k, l);
                                var isActive = currentMap.IsActive(i, j, k, l);
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

                                next[i][j][k][l] = isActive;
                            }
                        }
                    }
                }

                currentMap = next;
            }

            return currentMap;
        }
    }
}
