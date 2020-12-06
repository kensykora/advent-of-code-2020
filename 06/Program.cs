using System;
using System.IO;
using System.Linq;

namespace _06
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
            Console.WriteLine(
                File.ReadAllText("input.txt")
                    .Split("\r\n\r\n") // Split out groups (1 group per array element)
                    .Select(x => x.Replace("\r\n", null).Distinct()) // Ignore line breaks and get distinct answers
                    .Sum(x => x.Count()) // Sum the counts of the distinct answers
            );
        }

        static void Part2()
        {
            Console.WriteLine(
                File.ReadAllText("input.txt")
                    .Split("\r\n\r\n") // Split out groups (1 group per array element)
                    .Select(x => x.Split("\r\n")) // each group has its own string array, 1 person per element
                    .Sum(x => x.Aggregate(
                        // Get the running intersection of all answers which wil ultimately become
                        // The set of answers that everyone answered yes to
                        (cur, next) => new string(cur.ToArray().Intersect(next.ToArray()).ToArray())).Count()
                    )
            );
        }
    }
}
