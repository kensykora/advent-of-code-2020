using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace _12
{
    class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadLines("input.txt");
            Part1(input);
            Part2(input);
        }

        static void Part1(IEnumerable<string> input)
        {
            var direction = 90;
            var eastWestPos = 0;
            var northSouthPos = 0;

            var instructions = input.GetEnumerator();

            while (instructions.MoveNext())
            {
                var instruction = instructions.Current[0];
                var value = int.Parse(instructions.Current.Substring(1));

                switch (instruction)
                {
                    case 'N':
                        northSouthPos += value;
                        break;
                    case 'S':
                        northSouthPos -= value;
                        break;
                    case 'E':
                        eastWestPos += value;
                        break;
                    case 'W':
                        eastWestPos -= value;
                        break;
                    case 'L':
                        value = value % 360;
                        direction -= value;
                        direction = NormalizeDirection(direction);
                        break;
                    case 'R':
                        value = value % 360;
                        direction += value;
                        direction = NormalizeDirection(direction);
                        break;
                    case 'F':
                        switch (direction)
                        {
                            case 0:
                                northSouthPos += value;
                                break;
                            case 90:
                                eastWestPos += value;
                                break;
                            case 180:
                                northSouthPos -= value;
                                break;
                            case 270:
                                eastWestPos -= value;
                                break;
                            default:
                                throw new ApplicationException("Unexpected direction: " + direction);
                        }
                        break;
                    default:
                        throw new ApplicationException("Unexpected value: " + instruction);
                }
            }

            Console.WriteLine(Math.Abs(northSouthPos) + Math.Abs(eastWestPos));
        }

        static void Part2(IEnumerable<string> input)
        {
            var eastWestPos = 0;
            var northSouthPos = 0;

            var waypointNorthSouth = 1;
            var waypointEastWest = 10;

            var instructions = input.GetEnumerator();

            while (instructions.MoveNext())
            {
                var instruction = instructions.Current[0];
                var value = int.Parse(instructions.Current.Substring(1));
                var origNorthSouth = 0;

                switch (instruction)
                {
                    case 'N':
                        waypointNorthSouth += value;
                        break;
                    case 'S':
                        waypointNorthSouth -= value;
                        break;
                    case 'E':
                        waypointEastWest += value;
                        break;
                    case 'W':
                        waypointEastWest -= value;
                        break;
                    case 'R':
                        value = value % 360;
                        origNorthSouth = waypointNorthSouth;
                        switch(value)
                        {
                            case 360:
                            case 0:
                                // do nothing
                                break;
                            case 90:
                                waypointNorthSouth = -1 * waypointEastWest;
                                waypointEastWest = origNorthSouth;
                                break;
                            case 180:
                                waypointNorthSouth = -1 * waypointNorthSouth;
                                waypointEastWest = -1 * waypointEastWest;
                                break;
                            case 270:
                                waypointNorthSouth = waypointEastWest;
                                waypointEastWest = -1 * origNorthSouth;
                                break;
                        }
                        break;
                    case 'L':
                        value = value % 360;
                        origNorthSouth = waypointNorthSouth;
                        switch(value)
                        {
                            case 360:
                            case 0:
                                // do nothing
                                break;
                            case 90:
                                waypointNorthSouth = waypointEastWest;
                                waypointEastWest = -1 * origNorthSouth;
                                break;
                            case 180:
                                waypointNorthSouth = -1 * waypointNorthSouth;
                                waypointEastWest = -1 * waypointEastWest;
                                break;
                            case 270:
                                waypointNorthSouth = -1 * waypointEastWest;
                                waypointEastWest = origNorthSouth;
                                break;
                        }
                        break;
                    case 'F':
                        northSouthPos += value * waypointNorthSouth;
                        eastWestPos += value * waypointEastWest;
                        break;
                    default:
                        throw new ApplicationException("Unexpected value: " + instruction);
                }
            }

            Console.WriteLine(Math.Abs(northSouthPos) + Math.Abs(eastWestPos));
        }

        static int NormalizeDirection(int direction)
        {
            if (direction >= 360)
            {
                direction -= 360;
            }
            else if (direction < 0)
            {
                direction += 360;
            }

            return direction;
        }
    }
}
