﻿using System;
using System.IO;
using System.Linq;

namespace _05
{
    class Program
    {
        static void Main(string[] args)
        {
            // Console.WriteLine(new Seat("BFFFBBFRRR"));
            // Console.WriteLine(new Seat("FFFBBBFRRR"));
            // Console.WriteLine(new Seat("BBFFBBFRLL"));

            //Part1();
            Part2();
        }

        static void Part2()
        {
            var input = File.ReadLines("input.txt");
            var map = new Seat[128, 8];
            var minRow = 128;
            var maxRow = 0;

            foreach (var line in input)
            {
                var seat = new Seat(line.Trim());
                map[seat.Row, seat.Column] = seat;
                if (minRow > seat.Row)
                {
                    minRow = seat.Row;
                }

                if (maxRow < seat.Row)
                {
                    maxRow = seat.Row;
                }
            }

            for (var i = minRow + 1; i <= maxRow - 1; i++)
            {
                for (var j = 0; j < map.GetUpperBound(1); j++)
                {
                    if (map[i, j] == null)
                    {
                        Console.WriteLine(new Seat(i, j));
                    }
                }
            }
        }

        static void Part1()
        {
            var input = File.ReadLines("input.txt");
            var max = 0;
            foreach (var line in input)
            {
                var seat = new Seat(line.Trim());
                if (seat.SeatId > max)
                {
                    max = seat.SeatId;
                }
            }

            Console.WriteLine($"Max SeatId: {max}");
        }

        public class Seat
        {
            public Seat(string input)
            {
                Row = GetResult(input.Substring(0, 7), 128, 'F', 'B');
                Column = GetResult(input.Substring(7), 8, 'L', 'R');
            }

            public Seat(int row, int col)
            {
                Row = row;
                Column = col;
            }

            private static int GetResult(string input, int range, char lowerIndicator, char upperIndicator)
            {
                var upper = range;
                var lower = 0;

                for (var i = 0; i < input.Length - 1; i++)
                {
                    range = upper - lower;
                    if (input[i] == lowerIndicator)
                    {
                        upper -= range / 2;
                    }
                    else if (input[i] == upperIndicator)
                    {
                        lower += range / 2;
                    }

                }

                return input.ToCharArray().Last() == lowerIndicator ? lower : upper - 1;
            }

            public int Row;
            public int Column;
            public int SeatId => (Row * 8) + Column;

            public override string ToString()
            {
                return $"({Row}, {Column}) - {SeatId}";
            }
        }
    }
}
