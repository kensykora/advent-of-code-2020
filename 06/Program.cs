using System;
using System.IO;
using System.Linq;

namespace _06
{
    class Program
    {
        static void Main(string[] args)
        {
            Part1();
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
    }
}
