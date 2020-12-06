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
            var input = File.ReadAllText("input.txt");
            var groups = input.Split("\r\n\r\n");
            var groupData = groups.Select(x => x.Replace("\r\n", null).Distinct());
            
            var count = groupData.Sum(x => x.Count());
            Console.WriteLine(count);
        }
    }
}
