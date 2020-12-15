using System;
using System.IO;
using System.Linq;

namespace _11
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            var rows = input.Length;
            var cols = input[0].Length;
            var map = new bool?[rows, cols];

            for (var i = 0; i < rows; i++)
            {
                for (var j = 0; j < cols; j++)
                {
                    map[i, j] = input[i][j] == 'L' ? false : (bool?)null;
                }
            }

            Part1(map);
            Part2(map);
        }

        static void Part1(bool?[,] map)
        {
            var oldCount = -1;
            var newCount = 0;

            var current = map;

            while (oldCount != newCount)
            {
                var result = ApplyRound(current);
                oldCount = newCount;
                newCount = result.occupiedCount;
                current = result.result;
            }

            Console.WriteLine(newCount);
        }

        static void Part2(bool?[,] map)
        {
            var oldCount = -1;
            var newCount = 0;

            var current = map;

            while (oldCount != newCount)
            {
                var result = ApplyRoundPart2(current);
                oldCount = newCount;
                newCount = result.occupiedCount;
                current = result.result;
                //Print(current);
            }

            Console.WriteLine(newCount);
        }

        static (bool?[,] result, int occupiedCount) ApplyRound(bool?[,] map)
        {
            var result = new bool?[map.GetLength(0), map.GetLength(1)];
            var occupiedCount = 0;
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    if (map[i, j] == false) // If not occupied
                    {
                        if (CountAdjacentOccupiedSeats(map, i, j) == 0)
                        {
                            // become occupied
                            result[i, j] = true;
                            occupiedCount++;
                        }
                        else
                        {
                            // stay unoccupied
                            result[i, j] = false;
                        }
                    }
                    else if (map[i, j] == true) // If occupied
                    {
                        if (CountAdjacentOccupiedSeats(map, i, j) >= 4)
                        {
                            // become unoccupied
                            result[i, j] = false;
                        }
                        else
                        {
                            // stay occupied
                            result[i, j] = true;
                            occupiedCount++;
                        }
                    }
                    else
                    {
                        result[i, j] = null;
                    }
                }
            }

            return (result, occupiedCount);
        }

        static (bool?[,] result, int occupiedCount) ApplyRoundPart2(bool?[,] map)
        {
            var result = new bool?[map.GetLength(0), map.GetLength(1)];
            var occupiedCount = 0;
            for (var i = 0; i < result.GetLength(0); i++)
            {
                for (var j = 0; j < result.GetLength(1); j++)
                {
                    if (map[i, j] == false) // If not occupied
                    {
                        if (CountUnendingOccupiedSeats(map, i, j) == 0)
                        {
                            // become occupied
                            result[i, j] = true;
                            occupiedCount++;
                        }
                        else
                        {
                            // stay unoccupied
                            result[i, j] = false;
                        }
                    }
                    else if (map[i, j] == true) // If occupied
                    {
                        if (CountUnendingOccupiedSeats(map, i, j) >= 5)
                        {
                            // become unoccupied
                            result[i, j] = false;
                        }
                        else
                        {
                            // stay occupied
                            result[i, j] = true;
                            occupiedCount++;
                        }
                    }
                    else
                    {
                        result[i, j] = null;
                    }
                }
            }

            return (result, occupiedCount);
        }

        static int CountUnendingOccupiedSeats(bool?[,] map, int row, int col)
        {
            var result = 0;

            if (SeatOccupiedUnending(map, row, col, -1, -1)) result++;
            if (SeatOccupiedUnending(map, row, col, -1, 0)) result++;
            if (SeatOccupiedUnending(map, row, col, -1, 1)) result++;
            if (SeatOccupiedUnending(map, row, col, 0, -1)) result++;
            if (SeatOccupiedUnending(map, row, col, 0, 1)) result++;
            if (SeatOccupiedUnending(map, row, col, 1, -1)) result++;
            if (SeatOccupiedUnending(map, row, col, 1, 0)) result++;
            if (SeatOccupiedUnending(map, row, col, 1, 1)) result++;

            return result;
        }

        static int CountAdjacentOccupiedSeats(bool?[,] map, int row, int col)
        {
            var result = 0;

            if (SeatOccupied(map, row - 1, col - 1)) result++;
            if (SeatOccupied(map, row, col - 1)) result++;
            if (SeatOccupied(map, row + 1, col - 1)) result++;
            if (SeatOccupied(map, row - 1, col)) result++;
            // if (SeatOccupied(map, row, col)) result++; // Don't check your own seat
            if (SeatOccupied(map, row + 1, col)) result++;
            if (SeatOccupied(map, row - 1, col + 1)) result++;
            if (SeatOccupied(map, row, col + 1)) result++;
            if (SeatOccupied(map, row + 1, col + 1)) result++;

            return result;
        }

        static bool SeatOccupied(bool?[,] map, int row, int col)
        {
            if (
                row >= 0 && row < map.GetLength(0)
                && col >= 0 && col < map.GetLength(1)
                && map[row, col] == true)
            {
                return true;
            }

            return false;
        }

        static bool SeatOccupiedUnending(bool?[,] map, int row, int col, int deltaRow, int deltaCol)
        {
            var result = false;
            var currentRow = row + deltaRow;
            var currentCol = col + deltaCol;

            while (currentRow >= 0 && currentRow < map.GetLength(0)
                && currentCol >= 0 && currentCol < map.GetLength(1)
                && !map[currentRow, currentCol].HasValue)
            {
                currentRow += deltaRow;
                currentCol += deltaCol;
            }

            if (currentRow >= 0 && currentRow < map.GetLength(0)
                && currentCol >= 0 && currentCol < map.GetLength(1))
            {
                result = map[currentRow, currentCol] == true;
            }

            return result;
        }

        static void Print(bool?[,] map)
        {
            for (var i = 0; i < map.GetLength(0); i++)
            {
                for (var j = 0; j < map.GetLength(1); j++)
                {
                    Console.Write(map[i, j] == true ? '#' : (map[i, j] == false ? 'L' : '.'));
                }
                Console.Write('\n');
            }
            Console.WriteLine();
        }
    }
}
