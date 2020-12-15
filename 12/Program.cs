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
