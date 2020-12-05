using System;
using System.Linq;
using System.IO;
using System.Text.RegularExpressions;

namespace _02
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
            var input = File.ReadLines("input.txt");
            var validPasswords = 0;
            foreach (var line in input)
            {
                var capture = Regex.Match(line, "(?<low>\\d*)-(?<high>\\d*) (?<letter>[a-zA-Z])\\: (?<password>[a-zA-Z]*)");
                var low = int.Parse(capture.Groups["low"].Value);
                var high = int.Parse(capture.Groups["high"].Value);
                var letter = capture.Groups["letter"].Value[0];
                var password = capture.Groups["password"].Value;

                var count = password.Count(x => x == letter);
                if (count < low || count > high)
                {
                    Console.WriteLine("Bad: " + line);
                }
                else
                {
                    validPasswords++;
                }
            }

            Console.WriteLine("Valid Passwords: " + validPasswords);
        }

        static void Part2()
        {
            var input = File.ReadLines("input.txt");
            var validPasswords = 0;
            foreach (var line in input)
            {
                var capture = Regex.Match(line, "(?<posa>\\d*)-(?<posb>\\d*) (?<letter>[a-zA-Z])\\: (?<password>[a-zA-Z]*)");
                var posA = int.Parse(capture.Groups["posa"].Value);
                var posB = int.Parse(capture.Groups["posb"].Value);
                var letter = capture.Groups["letter"].Value[0];
                var password = capture.Groups["password"].Value;

                
                if (password[posA-1] == letter ^ password[posB-1] == letter)
                {
                    validPasswords++;
                }
                else
                {
                    Console.WriteLine("Bad: " + line);
                }
            }

            Console.WriteLine("Valid Passwords: " + validPasswords);
        }
    }
}
